<script setup lang="ts">
import { useTemplateRef } from 'vue';
import Fullscreen from '../utils/Fullscreen.vue';

const container = useTemplateRef('container')

function show() {
  container.value?.show();
}

function hide() {
  container.value?.hide()
}


defineExpose({
  show,
  hide,
})
</script>

<template>
  <Fullscreen ref="container">
    
  </Fullscreen>
</template>