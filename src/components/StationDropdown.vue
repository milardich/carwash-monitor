<script setup lang="ts">
import { defineProps, ref, watch } from 'vue';
import { type Station, getStation } from '@/api/station.api';

const props = defineProps<{
    stations: Station[]
}>();

const selectedStation = ref<Station | null>(null);
const stationDropdownOpen = ref(false);

// Toggle station dropdown
const toggleStationDropdown = () => {
    stationDropdownOpen.value = !stationDropdownOpen.value;
}

// Function to change selected station
async function changeSelectedStation(stationId: number) {
    selectedStation.value = await getStation(stationId);
}

// Watch for changes in props.stations and update selectedStation accordingly
watch(() => props.stations, (newStations) => {
    if (newStations.length > 0) {
        selectedStation.value = newStations[0];
    }

});

</script>

<template>
    <button id="dropdownDefaultButton" data-dropdown-toggle="dropdown" @click="toggleStationDropdown"
        class="px-5 py-2.5 text-center inline-flex bg-violet-dark text-white-light rounded-lg text-xl" type="button">

        <span v-if="selectedStation"> {{ selectedStation.stationName }} </span>
        <span v-else> - </span>

        <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
            viewBox="0 0 10 6">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="m1 1 4 4 4-4" />
        </svg>
    </button>

    <!-- Dropdown menu -->
    <div id="dropdown" :class="{ 'hidden': !stationDropdownOpen }"
        class="z-10 bg-white divide-y divide-gray-100 rounded-lg shadow w-44 absolute">
        <ul class="py-2 text-sm station-dropdown rounded-lg" aria-labelledby="dropdownDefaultButton">
            <a href="#" class="list-link">
                <li v-for="station in props.stations" class="block px-4 py-2"
                    @click="changeSelectedStation(station.stationId); toggleStationDropdown()">
                    {{ station.stationName }}
                </li>
            </a>
        </ul>
    </div>
</template>
