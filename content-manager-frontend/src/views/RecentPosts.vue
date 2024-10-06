<script setup lang="ts">
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
import * as controller from '@/lib/api/content-post-controller';
import type { ContentPostResponse } from '@/lib/api/models';
import { ref } from 'vue';
import ContentPostPreview from '@/components/misc/ContentPostPreview.vue';
import Page from '@/components/utils/Page.vue';


const defaultPage = 1
const itemsPerPage = 80

const totalItems = ref(itemsPerPage);
const pageContent = ref<ContentPostResponse[]>([])

updatePage(defaultPage);

function updatePage(value: number): void {
  controller.getCount().then(count => totalItems.value = count)
  pageContent.value = []
  getPage(value).then(array => pageContent.value = array);
}


async function getPage(page: number): Promise<ContentPostResponse[]> {
  return await controller.getPage({
    pageNumber: page,
    pageSize: itemsPerPage,
  });
}
</script>

<template>
  <Page title="RecentPosts">
    <div class="grid grid-cols-[repeat(auto-fill,300px)] gap-2 mb-8">
      <template v-for="item in pageContent">
        <ContentPostPreview :post_info="item" />
      </template>
    </div>

    <Pagination v-slot="{ page }" :items-per-page="itemsPerPage" :total="totalItems" :sibling-count="1" show-edges
      :default-page="defaultPage" @update:page="updatePage">
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
  </Page>
</template>