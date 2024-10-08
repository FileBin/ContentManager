<script setup lang="ts">
import { Menu, Search } from 'lucide-vue-next'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Sheet, SheetContent, SheetTrigger } from '@/components/ui/sheet'
import ThemeSwitch from '../theme-switch/ThemeSwitch.vue'
import AppIcon from './AppIcon.vue'
import HeaderUserWidget from '../user/HeaderUserWidget.vue'

const navLinks: { [id: string]: string } = {
  'Recent posts': '/posts/recent',
  'New post': '/post/create',
}

const AppName = 'ContentManager'
</script>

<template>
  <header class="z-40 sticky top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-6">
    <nav aria-label="header-nav"
      class="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
      <RouterLink to="/" class="flex items-center gap-2 text-lg font-semibold md:text-base">
        <AppIcon />
      </RouterLink>

      <RouterLink to="/"
        class="text-base text-accent-foreground transition-colors hover:text-primary-foreground text-nowrap">
        {{ AppName }}
      </RouterLink>

      <RouterLink v-for="(link, name) in navLinks" v-bind:to="link"
        class="text-muted-foreground transition-colors hover:text-foreground text-nowrap">
        {{ name }}
      </RouterLink>
    </nav>
    <Sheet>
      <SheetTrigger as-child>
        <Button variant="outline" size="icon" class="shrink-0 md:hidden">
          <Menu class="h-5 w-5" />
          <span class="sr-only">Toggle navigation menu</span>
        </Button>
      </SheetTrigger>
      <SheetContent side="left">
        <nav aria-label="sidebar-nav" class="grid gap-6 text-lg font-medium">
          <a href="#" class="flex items-center gap-2 text-lg font-semibold">
            <AppIcon />
          </a>

          <RouterLink to="/"
            class="text-base text-accent-foreground transition-colors hover:text-primary-foreground text-nowrap">
            {{ AppName }}
          </RouterLink>

          <RouterLink v-for="(link, name) in navLinks" v-bind:to="link"
            class="text-muted-foreground transition-colors hover:text-foreground text-nowrap">
            {{ name }}
          </RouterLink>
        </nav>
      </SheetContent>
    </Sheet>
    <div class="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
      <form class="ml-auto flex-1 sm:flex-initial" @submit.prevent="() => {}">
        <div class="relative">
          <Search class="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
          <Input type="search" placeholder="Search products..." class="pl-8 sm:w-[300px] md:w-[200px] lg:w-[300px]" />
        </div>
      </form>
      <ThemeSwitch />
      <HeaderUserWidget />
    </div>
  </header>
</template>