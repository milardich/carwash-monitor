<script setup lang="ts">
import { ref, watch } from 'vue';
import { getStation } from '@/api/station.api';

const stationDropdownOpen = ref(false);

const toggleFilterOptionDropdown = () => {
    stationDropdownOpen.value = !stationDropdownOpen.value;
}

const filters = ref(["Today", "Last 7 days", "This month"]);

const filterOption = ref("");

function changeSelectedFilterOption(option: string) {
    filterOption.value = option;
}


</script>

<template>
    <button id="dropdownDefaultButton" data-dropdown-toggle="dropdown" @click="toggleFilterOptionDropdown"
        class="px-5 py-2.5 text-center inline-flex bg-violet-dark text-white-light rounded-lg text-xl" type="button">

        <span v-if="filterOption == ''"> {{ filters[0] }} </span>
        <span v-else> {{ filterOption }} </span>


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
                <li v-for="option in filters"
                    @click="changeSelectedFilterOption(option); toggleFilterOptionDropdown();">
                    {{ option }} <option value=""></option>
                </li>
            </a>
        </ul>
    </div>

    <span> *Reload data button* </span>
</template>
