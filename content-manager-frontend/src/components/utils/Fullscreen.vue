<script setup lang="ts">
import { XIcon } from 'lucide-vue-next';
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

defineExpose({
  show,
  hide,
});

</script>



<template>
  <div @touchmove.prevent v-if="isOpen"
    class="flex z-[100] flex-col fixed top-0 left-0 w-screen h-screen bg-[#000000c0]">
    <Button class="z-[110] fixed w-max h-max p-4 top-4 right-4 opacity-75" @click="hide" variant="ghost">
      <XIcon class="w-8 h-8" />
    </Button>
    <div class="w-screen h-screen">
      <slot />
    </div>
  </div>
</template>