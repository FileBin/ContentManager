<script setup lang="ts">
import ContentViewer from '@/components/misc/ContentViewer.vue';
import { Skeleton } from '@/components/ui/skeleton';
import Fullscreen from '@/components/utils/Fullscreen.vue';
import Page from '@/components/utils/Page.vue';
import { getPreviewUrlByUuid } from '@/lib/api/content-controller';

import * as controller from '@/lib/api/content-post-controller';
import { Quality, type ContentPostResponse } from '@/lib/api/models';
import router from '@/router';
import { ref, type Ref } from 'vue';

const postId = router.currentRoute.value.params.postId as string;

let post: Ref<ContentPostResponse | null> = ref(null);
let variant = 1;

controller.getPost(postId).then(resp => post.value = resp);
</script>

<template>
  <template v-if="post === null">
    <div class="flex flex-col space-y-3">
      <Skeleton class="h-[125px] w-[250px] rounded-xl" />
      <div class="space-y-2">
        <Skeleton class="h-4 w-[250px]" />
        <Skeleton class="h-4 w-[200px]" />
      </div>
    </div>
  </template>
  <template v-else>
    <Page :title="post.name" :description="post.description">
      <div class="flex">
        <template v-for="(item, index) in post.contentVariants">
          <img @click="() => $refs.viewer?.show(index, variant)" class="w-full h-full object-contain mx-auto transition-all hover:brightness-75"
            :alt="`${post.name}_p${index}_v${variant}`" :src="getPreviewUrlByUuid(item[variant], Quality._2k)">
        </template>
      </div>
    </Page>
    <ContentViewer ref="viewer" :post="post" />
  </template>
</template>