<script setup lang="ts">
import { getPreviewUrlByUuid } from '@/lib/api/content-controller';
import type { ContentPostResponse } from '@/lib/api/models';
import { Quality } from '@/lib/api/models';

interface Props {
    post_info: ContentPostResponse
}

const props = defineProps<Props>()
</script>

<template>
    <div class="flex text-lg font-semibold w-full flex-col">
        <div class="flex w-full justify-center">
            <img class="h-[200px] w-full object-cover" :alt="`${post_info.name}_img`"
                :src="getPreviewUrlByUuid(post_info.previewId, Quality.medium)">
        </div>
        <p>
            {{ post_info.name }}
        </p>
    </div>
</template>