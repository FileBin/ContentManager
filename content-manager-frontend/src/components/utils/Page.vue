<script setup lang="ts">
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'

interface PageProps {
  title: string,
  description?: string,
}

const props = defineProps<PageProps>()
</script>

<template>
  <Card class="mb-0 gap-8">
    <CardHeader class="h-16 mb-8">
      <CardTitle class="text-2xl">
        {{ title }}
      </CardTitle>
      <CardDescription v-if="description">
        {{ description }}
      </CardDescription>
    </CardHeader>
    <CardContent class="flex flex-col justify-between w-full min-h-[calc(100%-4rem-2rem)]">
      <slot />
    </CardContent>
  </Card>
</template>