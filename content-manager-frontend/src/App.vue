<script setup lang="ts">
import { RouterView } from 'vue-router'
import AppHeader from './components/app-header/AppHeader.vue';
</script>

<template>
  <div class="flex min-h-screen w-full flex-col">
    <AppHeader />
    <main class="flex flex-col gap-2 h-[calc(100vh-4rem)] m-0">
      <RouterView />
    </main>
  </div>
</template>
