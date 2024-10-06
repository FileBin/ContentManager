<script setup lang="ts">
import { getPreviewUrlByUuid } from '@/lib/api/content-controller';
import type { ContentPostResponse } from '@/lib/api/models';
import { Quality } from '@/lib/api/models';
import router from '@/router';

interface Props {
    post_info: ContentPostResponse
}

const props = defineProps<Props>()

function goToPost() {
    router.push({ path: `/posts/by-id/${props.post_info.id}` })
}
</script>

<template>
    <div class="flex text-lg font-semibold w-full flex-col group" @click="goToPost">
        <div class="flex w-full justify-center">
            <img class="h-[200px] w-full object-cover transition-all group-hover:brightness-75"
                :alt="`${post_info.name}_img`" :src="getPreviewUrlByUuid(post_info.previewId, Quality.small)">
        </div>
        <p>
            {{ post_info.name }}
        </p>
    </div>
</template>