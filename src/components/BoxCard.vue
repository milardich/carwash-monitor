<script lang="ts" setup>
import { defineComponent, onBeforeMount, onBeforeUnmount, onMounted, ref } from 'vue';
import { type BoxInfo, type Box, getBoxInfo, getBox } from '@/api/box.api';
import { useBoxStore } from '@/stores/boxStore';
import { useStationStore } from '@/stores/stationStore';
import { strDateTime, strDateTimeMidnight } from '@/util/dateTimeUtils';
import { watch } from 'vue';

const boxStore = useBoxStore();


const props = defineProps({
    box: null
});

var boxInfo = ref<BoxInfo>();
const stationStore = useStationStore();

const stationId = stationStore?.selectedStation!.id;

try {
    boxInfo.value = await getBoxInfo(props?.box?.id);
} catch (error) {
    console.error("Failed to fetch box  info:", error);
}

// Colors for IN_USE, AVAILABLE and INACTIVE labels
const backgroundColorCssClass = ref<string>("bg-yellow-warning");
const boxStateLabel = ref<string>(props?.box?.status);

function setBoxStatusProperties(boxState: string) {
    if (boxState == "AVAILABLE") {
        backgroundColorCssClass.value = "available-bg-color";
        boxStateLabel.value = boxState;
    }
    else if (boxState == "IN_USE") {
        backgroundColorCssClass.value = "in-use-bg-color";
        boxStateLabel.value = boxState;
    }
    else if (boxState == "INACTIVE") {
        backgroundColorCssClass.value = "inactive-bg-color"
        boxStateLabel.value = boxState;
    }
}

// TODO: mby put in on mounted or whatever
// set label and color when page loads
setBoxStatusProperties(props.box.status);

var intervalId: number;

onMounted(async () => {



    // intervalId = window.setInterval(async () => {
    //     try {
    //         boxInfo.value = await getBoxInfo(props?.box?.id);
    //     } catch (error) {
    //         throw (error);
    //     }
    // }, 30000);
});

// ?? probably unnecessay
onBeforeUnmount(() => {
    clearInterval(intervalId);
});

// watch if status changed through UI
// ?? probably not needed cuz (can use ref)
// TODO: ask chatgpt about industry standard
watch(
    () => props?.box?.status,
    (status) => {
        setBoxStatusProperties(status);
    }
);

// watch if status fetched from api is updated
watch(
    () => boxInfo?.value?.status,
    (newBoxStatus) => {
        if (newBoxStatus !== undefined) {
            setBoxStatusProperties(newBoxStatus);
        }
    }
);
</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-xl p-4 content-between content-small-container-color">
        <div class="flex">
            <div class="text-3xl"> #{{ box.number }} </div>
            <div class="ml-auto justify-end">
                <button @click="boxStore.setSelectedBox(box); boxStore.updateBoxInfo(box); boxStore.toggleBoxPopup();">
                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset="" class="card-button-icon-small">
                </button>
            </div>
        </div>

        <div class="mt-2">
            <div class="grid grid-cols-1 text-sm">
                <div v-if="boxInfo">
                    <div>Washes today: <span class="font-bold">{{ boxInfo.washCycleCount }}</span></div>
                    <div>Coin tray: <span class="font-bold">{{ boxInfo.totalCoinAmount }}</span></div>
                </div>
                <div v-else>
                    <div> Loading info... </div>
                </div>
            </div>
        </div>

        <div class="border-1 border-black rounded-xl text-center p-1" :class="backgroundColorCssClass">
            {{ boxStateLabel }}
        </div>
    </div>
</template>