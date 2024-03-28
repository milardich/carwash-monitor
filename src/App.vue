<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import SideMenu from './components/SideMenu.vue'
</script>

<template>
  <div class="p-4 sm:ml-64">
    <div>
      <SideMenu />
    </div>
    <div>
      <RouterView />
    </div>
  </div>
</template>
