<script setup lang="ts">
import { FullscreenIcon, XIcon } from 'lucide-vue-next';
import Button from '../ui/button/Button.vue';
import { ref } from 'vue';

let isOpen = ref(false);

function show() {
  document.documentElement.style.overflow = 'hidden'
  isOpen.value = true;
}

function hide() {
  document.documentElement.style.overflow = 'auto'
  isOpen.value = false;
}

function toggleFullScreen() {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen();
  } else if (document.exitFullscreen) {
    document.exitFullscreen();
  }
}

defineExpose({
  show,
  hide,
  toggleFullScreen,
});

</script>



<template>
  <div @touchmove.prevent v-if="isOpen"
    class="flex z-[100] flex-col fixed top-0 left-0 w-screen h-screen bg-[#000000c0]">
    <div class="z-[110] fixed top-4 right-4 flex">
      <Button class="w-max h-max p-4 opacity-75" @click="toggleFullScreen" variant="ghost">
        <FullscreenIcon class="w-8 h-8" />
      </Button>

      <Button class="w-max h-max p-4 opacity-75" @click="hide" variant="ghost">
        <XIcon class="w-8 h-8" />
      </Button>
    </div>
    <div class="w-screen h-screen">
      <slot />
    </div>
  </div>
</template>