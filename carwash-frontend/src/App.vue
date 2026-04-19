<script setup lang="ts">
import { ref } from 'vue'
import { RouterView } from 'vue-router'
import SideMenu from './components/SideMenu.vue'

const sidebarOpen = ref(false)
</script>

<template>
    <div class="min-h-screen">
        <!-- Mobile top bar -->
        <header class="lg:hidden fixed top-0 left-0 right-0 z-30 flex items-center gap-3 px-4 h-14 bg-white shadow-md">
            <button @click="sidebarOpen = true"
                class="p-2 -ml-2 rounded-lg text-gray-700 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                aria-label="Open menu" aria-controls="default-sidebar" :aria-expanded="sidebarOpen">
                <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"
                    xmlns="http://www.w3.org/2000/svg">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
                </svg>
            </button>
            <span class="font-semibold text-lg">Carwash Monitor</span>
        </header>

        <!-- Backdrop for mobile drawer -->
        <div v-if="sidebarOpen" @click="sidebarOpen = false"
            class="lg:hidden fixed inset-0 z-30 bg-black/50 backdrop-blur-[2px]" aria-hidden="true"></div>

        <SideMenu :open="sidebarOpen" @close="sidebarOpen = false" />

        <main class="lg:ml-64 min-h-screen pt-14 lg:pt-0">
            <div class="p-3 sm:p-5">
                <RouterView />
            </div>
        </main>
    </div>
</template>
