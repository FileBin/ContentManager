<script setup lang="ts">
import { ref, useTemplateRef, type Ref } from 'vue';
import Fullscreen from '../utils/Fullscreen.vue';
import { getContentUrlByUuid } from '@/lib/api/content-controller';
import type { ContentPostResponse } from '@/lib/api/models';
import PinchScrollZoom from '@coddicat/vue-pinch-scroll-zoom';
import PictureViewer from '../utils/PictureViewer.vue';

const container = useTemplateRef('container')
//const zoomContainer = useTemplateRef('zoomContainer')

interface ViewerProps {
  post: ContentPostResponse
}

const props = defineProps<ViewerProps>()

function show(_order?: number, _variant?: number) {
  if (_order) order = _order
  if (_variant) variant = _variant
  container.value?.show();
}

function hide() {
  container.value?.hide()
}


defineExpose({
  show,
  hide,
})

//TODO make own picture touch zoom with rotation
let order = 0;
let variant = 0;

const w = ref(window.innerWidth)
const h = ref(window.innerHeight)
</script>

<template>
  <Fullscreen ref="container">
    <PictureViewer :url="getContentUrlByUuid(post.contentVariants[order][variant])" />

    <!-- <PinchScrollZoom ref="zoomContainer" within centered key-actions :width="w" :height="h" :min-scale="0.1"
      :max-scale="100" style="width: 100vw; height: 100vh">
      <img v-on:touchmove="e => console.log(e)" :draggable="false" class="w-screen h-screen object-contain"
        alt="Loading..." :src="getContentUrlByUuid(post.contentVariants[order][variant])">
    </PinchScrollZoom> -->
  </Fullscreen>
</template>