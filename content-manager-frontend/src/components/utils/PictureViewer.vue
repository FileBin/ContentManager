<script setup lang="ts">
import { one, translate2d, rotate2d, scale2d } from '@/lib/utils';
import { array, multiply, scale, zeros, type NDArray } from 'vectorious';
import { ref, useTemplateRef } from 'vue';
interface ViewContainerProps {
  url: string
}

defineProps<ViewContainerProps>()

const rect = useTemplateRef('rect')

let transform = one()

const style = ref('')

updateStyle();

function updateStyle() {
  style.value = `matrix(${transform.data[0]}, ${transform.data[1]}, ${transform.data[3]}, ${transform.data[4]}, ${transform.data[6]}, ${transform.data[7]})`
}

function handleMouseMoveEvent(e: MouseEvent) {
  const client_rect = rect.value?.getBoundingClientRect()
  if (client_rect) {
    let button = (e.buttons | e.button)
    if (button === 1) {
      if (e.shiftKey) {
        handleRotateOnePoint(array([e.clientX, e.clientY]), client_rect)
      } else {
        transform = transform.multiply(translate2d(e.movementX, e.movementY))
      }
      updateStyle();
    }
  }
}

function handleMouseScrollEvent(e: WheelEvent) {
  const delta = - e.deltaY / 1000
  if (e.shiftKey) {
    transform = transform.multiply(rotate2d(delta * Math.PI / 6))
  } else {
    const client_rect = rect.value?.getBoundingClientRect();
    if (client_rect) {
      const x = e.clientX - client_rect.left; //x position within the element.
      const y = e.clientY - client_rect.top;  //y position within the element.
      const offsetX = x - client_rect.width * 0.5;
      const offsetY = y - client_rect.height * 0.5;
      const factor = Math.pow(2, delta);
      const mat = translate2d(offsetX, offsetY);
      const inv = translate2d(-offsetX, -offsetY);
      transform = transform
        .multiply(inv)
        .multiply(scale2d(factor))
        .multiply(mat)
    }
  }
  updateStyle();
}

function handleTouchEvent(e: TouchEvent) {
  const client_rect = rect.value?.getBoundingClientRect();
  if (client_rect) {
    if (e.touches.length == 1) {
      const touch = array([e.touches[0].clientX, e.touches[0].clientY]);
      if (tapsNumber == 2) {
        handleOnePoint(touch, client_rect)
      }
      if (tapsNumber == 3) {
        handleRotateOnePoint(touch, client_rect);
      }
    } else if (e.touches.length == 2) {
      const touches = [
        array([e.touches[0].clientX, e.touches[0].clientY]),
        array([e.touches[1].clientX, e.touches[1].clientY]),
      ]

      handleTwoPoints(touches, client_rect);
    }
  }
}

let previousTouches: NDArray[] = []
let anchor: NDArray | null = null

function handleOnePoint(point: NDArray, client_rect: DOMRect) {
  point = point.subtract(array([client_rect.left, client_rect.top]))
  if (previousTouches.length === 1) {
    const prev = previousTouches[0];
    const move = point.copy().subtract(prev)
    transform = transform.multiply(translate2d(move.data[0], move.data[1]))
    updateStyle();
  }
  previousTouches = [point];
}

function handleRotateOnePoint(point: NDArray, client_rect: DOMRect) {
  if (anchor === null) {
    anchor = array([client_rect.width, client_rect.height]).scale(0.5)

    anchor.x += transform.data[6]
    anchor.y += transform.data[7]

    anchor.add(array([client_rect.left, client_rect.top]));
  }
  const dist = anchor.copy().subtract(point);
  if (dist.dot(dist) > 10) {
    handleTwoPoints([anchor.copy().add(dist), anchor.copy().subtract(dist)], client_rect, false);
  }
}

function handleTwoPoints(points: NDArray[], client_rect: DOMRect, scale = true, rotate = true, translate = true) {
  const touches = points.map(p => p.subtract(array([client_rect.left, client_rect.top])))
  if (previousTouches.length > 1) {
    const center = touches[0].copy()
      .add(touches[1])
      .scale(0.5)
      .subtract(array([client_rect.width * 0.5, client_rect.height * 0.5]))

    const mat = translate2d(center.data[0], center.data[1]);
    const inv = translate2d(-center.data[0], -center.data[1]);

    let vectors = [touches, previousTouches]
      .map(t => t[0].copy().subtract(t[1]).copy());

    const sizes = vectors
      .map(t => Math.sqrt(t.dot(t)))

    vectors = vectors
      .map(v => v.normalize())
      .map(v => array([v.data[0], v.data[1], 0]));


    if (scale) {
      if (sizes[0] > 0 && sizes[1] > 0) {
        const scale = sizes[0] / sizes[1];


        transform = transform
          .multiply(inv)
          .multiply(scale2d(scale))
          .multiply(mat);
      }
    }
    if (rotate) {
      let rotation = vectors[0].cross(vectors[1]).data[2];
      rotation = Math.asin(rotation);

      transform = transform
        .multiply(inv)
        .multiply(rotate2d(rotation))
        .multiply(mat);
    }

    if (translate) {
      const all_touches = touches.map((t, i) => [t, previousTouches[i]])
      const moves = all_touches.map(t => t[0].copy().subtract(t[1]))
      const movement = moves[0].add(moves[1]).scale(0.5);

      transform = transform.multiply(translate2d(movement.data[0], movement.data[1]))
    }
    updateStyle();
  }
  previousTouches = [...touches]
}

let tapsNumber = 1

function handleTouchEndEvent() {
  previousTouches = []
  anchor = null;
}

let myLatestTap = new Date().getTime();
let myLatestTapPos = array([0, 0])

function doubleTap(e: PointerEvent) {

  const now = new Date().getTime()
  const timesince = now - myLatestTap

  const pos = array([e.clientX, e.clientY])

  if (timesince < 600 && pos.copy().subtract(myLatestTapPos).norm() < 50) {
    tapsNumber += 1
  } else {
    tapsNumber = 1;
  }

  console.log(tapsNumber)

  myLatestTap = new Date().getTime();
  myLatestTapPos = pos
}

</script>

<template>
  <div ref="rect" @mousemove="handleMouseMoveEvent" @wheel="handleMouseScrollEvent" @touchmove="handleTouchEvent"
    @touchend="handleTouchEndEvent" @mouseup="handleTouchEndEvent" @pointerdown="doubleTap"
    @pointerup="e => myLatestTapPos = array([e.clientX, e.clientY])" @touchcancel="tapsNumber = 1">
    <img v-bind:style="{ transform: style }" id="image" :draggable="false" class="w-screen h-screen object-contain"
      alt="Loading..." :src="url">
  </div>
</template>