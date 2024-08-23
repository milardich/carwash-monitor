<script lang="ts" setup>
import { defineComponent, onBeforeMount, onBeforeUnmount, onMounted, ref } from 'vue';
import { type UnitInfo, type Unit, getUnitInfo, getUnit } from '@/api/unit.api';
import { useUnitStore } from '@/stores/unitPopup';
import { useStationStore } from '@/stores/stationStore';
import { strDateTime, strDateTimeMidnight } from '@/util/dateTimeUtils';
import { watch } from 'vue';

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

function setUnitStatusProperties(unitState: string) {
    if (unitState == "AVAILABLE") {
        backgroundColorCssClass.value = "available-bg-color";
        unitStateLabel.value = unitState;
    }
    else if (unitState == "IN_USE") {
        backgroundColorCssClass.value = "in-use-bg-color";
        unitStateLabel.value = unitState;
    }
    else if (unitState == "INACTIVE") {
        backgroundColorCssClass.value = "inactive-bg-color"
        unitStateLabel.value = unitState;
    }
}

// set label and color when page loads
setUnitStatusProperties(props.unit.status);

var intervalId: number;

onMounted(() => {
    intervalId = window.setInterval(async () => {
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

// watch if status changed through UI
watch(
    () => props.unit.status,
    (status) => {
        setUnitStatusProperties(status);
    }
);

// watch if status fetched from api is updated
watch(
    () => unitInfo.value?.unitStatus,
    (newUnitStatus) => {
        if (newUnitStatus !== undefined) {
            setUnitStatusProperties(newUnitStatus);
        }
    }
);
</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-xl p-4 content-between content-small-container-color">
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
            <div class="grid grid-cols-1 text-sm">
                <div v-if="unitInfo">
                    <div>Washes today: <span class="font-bold">{{ unitInfo.washCycleCount }}</span></div>
                    <div>Coin tray: <span class="font-bold">{{ unitInfo.totalCoinAmount }}</span></div>
                </div>
                <div v-else>
                    <div> Loading info... </div>
                </div>
            </div>
        </div>

        <div class="border-1 border-black rounded-xl text-center p-1" :class="backgroundColorCssClass">
            {{ unitStateLabel }}
        </div>
    </div>
</template>