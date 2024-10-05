<script setup lang="ts">
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
</script>

<template>
  <Card class="w-full max-w-sm">
    <CardHeader>
      <CardTitle class="text-2xl">
        Home page
      </CardTitle>
      <CardDescription>
        This is home page
      </CardDescription>
    </CardHeader>
    <CardContent class="grid gap-4">
      ContentManager lorem ipsum...
    </CardContent>
  </Card>
</template>
