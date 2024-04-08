<script setup lang="ts">

import { ref } from 'vue';
import type { Unit } from '@/api/unit.api';
import { setSelectedUnit, toggleUnitPopup } from '@/stores/unitPopup';

const props = defineProps<{
    unit: Unit
}>()

const backgroundColorCssClass = ref<string>("bg-yellow-warning");
const unitStateLabel = ref<string>(props.unit.status);

switch (props.unit.status) {
    case "AVAILABLE":
        backgroundColorCssClass.value = "bg-green-light";
        break
    case "IN_USE":
        backgroundColorCssClass.value = "bg-red-light";
        break
    case "INACTIVE":
        backgroundColorCssClass.value = "bg-yellow-warning"
        break
}

// for testing purposes
function toInUse() {
    backgroundColorCssClass.value = "bg-red-light";
    unitStateLabel.value = "IN USE";
}

function toAvailable() {
    backgroundColorCssClass.value = "bg-green-light";
    unitStateLabel.value = "AVAILABLE";
}

function toInactive() {
    backgroundColorCssClass.value = "bg-yellow-warning";
    unitStateLabel.value = "INACTIVE";
}
///
</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between bg-white-light">
        <div class="flex">
            <div class="text-3xl"> #{{ unit.unitId }} </div>
            <div class="ml-auto justify-end">
                <button @click="toggleUnitPopup(); setSelectedUnit(unit);">
                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset="" class="card-button-icon-small">
                </button>
            </div>
        </div>

        <div class="mt-2">
            <div class="grid grid-cols-2 text-sm">
                <div>Washes today:</div>
                <div class="font-bold"> </div>
                <div>Coin tray:</div>
                <div class="font-bold"> {{ unit.coinTrayAmount }}% </div>
            </div>
        </div>

        <div class="border-1 border-black rounded-lg text-center p-1" :class="backgroundColorCssClass">
            {{ unitStateLabel }}
        </div>

        <!-- for testing purposes -->

        <!-- <button @click="toAvailable">toAvailable</button>
        <button @click="toInactive">toInactive</button>
        <button @click="toInUse">toInUse</button> -->

    </div>
</template>