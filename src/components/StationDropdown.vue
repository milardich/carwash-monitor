<script setup lang="ts">
import { ref } from 'vue'
import { useStationStore } from '@/stores/stationStore'

const stationDropdownOpen = ref(false)
const stationStore = useStationStore()

const toggleStationDropdown = () => {
    stationDropdownOpen.value = !stationDropdownOpen.value
}
</script>

<template>
    <div class="relative inline-block">
        <!-- Dropdown Button -->
        <button @click="toggleStationDropdown"
            class="px-5 py-2.5 inline-flex items-center justify-between bg-white border border-gray-300 rounded-lg shadow-sm text-xl font-medium hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="button">
            <span v-if="stationStore.selectedStation">
                {{ stationStore.selectedStation.name }}
            </span>
            <span v-else>
                {{ stationStore.stations[0]?.name }}
            </span>

            <svg class="w-3 h-3 ml-2 text-gray-600" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6"
                aria-hidden="true">
                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="m1 1 4 4 4-4" />
            </svg>
        </button>

        <!-- Dropdown menu -->
        <div v-if="stationDropdownOpen"
            class="absolute right-0 mt-2 w-48 bg-white rounded-lg shadow-lg ring-1 ring-black ring-opacity-5 z-20">
            <ul class="py-1 text-sm text-gray-700">
                <li v-for="station in stationStore.stations" :key="station.id">
                    <button @click="stationStore.switchStation(station); toggleStationDropdown();"
                        class="w-full text-left px-4 py-2 hover:bg-indigo-100 rounded-md">
                        {{ station.name }}
                    </button>
                </li>
            </ul>
        </div>
    </div>
</template>
