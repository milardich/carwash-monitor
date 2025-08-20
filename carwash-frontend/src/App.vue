<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import SideMenu from './components/SideMenu.vue'
</script>

<template>
  <div class="sm:ml-64 h-screen p-5">
    <div>
      <SideMenu />
    </div>
    <div class="h-full">
      <RouterView />
    </div>
  </div>
</template>