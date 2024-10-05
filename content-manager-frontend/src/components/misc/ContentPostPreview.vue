<script setup lang="ts">
import { getContentUrlByUuid } from '@/lib/api/content-controller';
import type { ContentPostResponse } from '@/lib/api/models';

interface Props {
    post_info: ContentPostResponse
}

const props = defineProps<Props>()
</script>

<template>
    <img :alt="post_info.name" :url="getContentUrlByUuid(post_info.preview_uuid)">
</template>