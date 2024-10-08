<script setup lang="ts">
import { XIcon } from 'lucide-vue-next';
import Button from '../ui/button/Button.vue';
import { ref } from 'vue';

let isOpen = ref(false);

function show() {
  isOpen.value = true;
}

function hide() {
  isOpen.value = false;
}

defineExpose({
  show,
  hide,
});

</script>



<template>
  <div v-if="isOpen" class="flex z-[100] flex-col fixed top-0 left-0 w-screen h-screen bg-black opacity-75">
    <slot />
    <Button class="fixed top-10 right-10" @click="hide" variant="ghost">
      <XIcon />
    </Button>
  </div>
</template>