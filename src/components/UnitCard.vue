<script lang="ts" setup>
import { defineComponent, onBeforeMount, onBeforeUnmount, onMounted, ref } from 'vue';
import { type UnitInfo, type Unit, getUnitInfo } from '@/api/unit.api';
import { useUnitStore } from '@/stores/unitPopup';
import { useStationStore } from '@/stores/stationStore';
import { strDateTime, strDateTimeMidnight } from '@/util/dateTimeUtils';

const unitStore = useUnitStore();


const props = defineProps({
    unit: null
});

var unitInfo = ref<UnitInfo>();
const stationStore = useStationStore();

const stationId = stationStore.selectedStation?.stationId;

try {
    unitStore.dateTo = strDateTime(new Date());
    unitStore.dateFrom = strDateTimeMidnight(new Date());
    unitInfo.value = await getUnitInfo(unitStore.dateFrom, unitStore.dateTo, stationId, props.unit.unitId);
} catch (error) {
    console.error("Failed to fetch unit info:", error);
}


// Colors for IN_USE, AVAILABLE and INACTIVE labels
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

var intervalId: number = 0;

onMounted(() => {
    intervalId = setInterval(async () => {
        var currentDate = new Date();
        unitStore.dateTo = strDateTime(currentDate);
        unitStore.dateFrom = strDateTimeMidnight(currentDate);
        try {
            unitInfo.value = await getUnitInfo(unitStore.dateFrom, unitStore.dateTo, stationId, props.unit.unitId);
        } catch (error) {
            throw (error);
        }
    }, 5000);
});

onBeforeUnmount(() => {
    clearInterval(intervalId);
});
</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between bg-white-light">
        <div class="flex">
            <div class="text-3xl"> #{{ unit.unitId }} </div>
            <div class="ml-auto justify-end">
                <button
                    @click="unitStore.setSelectedUnit(unit); unitStore.updateUnitInfo(unit); unitStore.toggleUnitPopup();">
                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset="" class="card-button-icon-small">
                </button>
            </div>
        </div>

        <div class="mt-2">
            <div class="grid grid-cols-2 text-sm">
                <div v-if="unitInfo">
                    <div>Washes today:</div>
                    <div class="font-bold"> {{ unitInfo.washCycleCount }} </div>
                    <div>Coin tray:</div>
                    <div class="font-bold"> {{ unitInfo.totalCoinAmount }} </div>
                </div>
                <div v-else>
                    <div> Loading info... </div>
                </div>
            </div>
        </div>

        <div class="border-1 border-black rounded-lg text-center p-1" :class="backgroundColorCssClass">
            {{ unitStateLabel }}
        </div>
    </div>
</template>