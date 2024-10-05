<script setup lang="ts">
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import {
  Pagination,
  PaginationEllipsis,
  PaginationFirst,
  PaginationLast,
  PaginationList,
  PaginationListItem,
  PaginationNext,
  PaginationPrev,
} from '@/components/ui/pagination'

import {
  Button,
} from '@/components/ui/button'
</script>

<template>
  <Card class="w-screen">
    <CardHeader>
      <CardTitle class="text-2xl">
        RecentPosts page
      </CardTitle>
    </CardHeader>
    <CardContent class="w-full">
      <Pagination v-slot="{ page }" :items-per-page="80" :total="1000" :sibling-count="1" show-edges :default-page="2"
      class = "m-auto">
        <PaginationList v-slot="{ items }" class="flex justify-center items-center gap-1">
          <PaginationFirst />
          <PaginationPrev />

          <template v-for="(item, index) in items">
            <PaginationListItem v-if="item.type === 'page'" :key="index" :value="item.value" as-child>
              <Button class="w-10 h-10 p-0" :variant="item.value === page ? 'default' : 'outline'">
                {{ item.value }}
              </Button>
            </PaginationListItem>
            <PaginationEllipsis v-else :key="item.type" :index="index" />
          </template>

          <PaginationNext />
          <PaginationLast />
        </PaginationList>
      </Pagination>
    </CardContent>
  </Card>
</template>