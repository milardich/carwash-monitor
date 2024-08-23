<script setup lang="ts">
import { ref, watch } from 'vue';
import { getStation } from '@/api/station.api';
import { useStationStore } from '@/stores/stationStore';
import { useResourceStore } from '@/stores/resourceStore';

const stationDropdownOpen = ref(false);

const stationStore = useStationStore();
const resourceStore = useResourceStore();

const toggleStationDropdown = () => {
    stationDropdownOpen.value = !stationDropdownOpen.value;
}

async function changeSelectedStation(stationId: number) {
    stationStore.selectedStation = undefined; // fixes incorrect unit rendering for some reason
    stationStore.selectedStation = await getStation(stationId);
    if (stationStore.selectedStation) {
        resourceStore.setChartDataByStationId(stationStore.selectedStation.stationId);
    }
}


</script>

<template>
    <button id="dropdownDefaultButton" data-dropdown-toggle="dropdown" @click="toggleStationDropdown"
        class="px-5 py-2.5 text-center inline-flex select-station-big-button rounded-lg text-xl" type="button">

        <span v-if="stationStore.selectedStation != null"> {{ stationStore.selectedStation.stationName }} </span>
        <span v-else> {{ stationStore.stations[0].stationName }} </span>

        <svg class="w-2.5 h-2.5 ms-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
            viewBox="0 0 10 6">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="m1 1 4 4 4-4" />
        </svg>
    </button>

    <!-- Dropdown menu -->
    <div id="dropdown" :class="{ 'hidden': !stationDropdownOpen }"
        class="z-10 divide-y divide-gray-100 rounded-lg shadow w-40 absolute select-station-dropdown shadow-xl">
        <ul class="text-sm rounded-lg" aria-labelledby="dropdownDefaultButton">
            <a href="#" class="list-link">
                <li v-for="station in stationStore.stations" class="block px-4 py-2 dropdown-item rounded-lg"
                    @click="changeSelectedStation(station.stationId); toggleStationDropdown()">
                    {{ station.stationName }}
                </li>
            </a>
        </ul>
    </div>
</template>
