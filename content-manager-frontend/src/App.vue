<script setup lang="ts">
import { RouterView } from 'vue-router'
import AppHeader from './components/app-header/AppHeader.vue';
</script>

<template>
  <div class="flex min-h-screen w-full flex-col">
    <AppHeader />
    <main class="flex ">
      <RouterView />
    </main>
  </div>
</template>
