<script lang="ts">
import { defineComponent, ref } from 'vue';
import { type UnitInfo, type Unit, getUnitInfo } from '@/api/unit.api';
import { setSelectedUnit, toggleUnitPopup } from '@/stores/unitPopup';
import { stationStore } from '@/stores/stationStore';

export default defineComponent({
    props: {
        unit: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const unitInfo = ref<UnitInfo | null>(null);

        // construct dateTimeFrom and dateTimeTo
        var currentDate = new Date();
        var month = currentDate.getMonth() < 10 ? "0" + (currentDate.getMonth() + 1) : currentDate.getMonth();
        var day = currentDate.getDate() < 10 ? "0" + currentDate.getDate() : currentDate.getDate();
        var hour = currentDate.getHours() < 10 ? "0" + currentDate.getHours() : currentDate.getHours();
        var minute = currentDate.getMinutes() < 10 ? "0" + currentDate.getMinutes() : currentDate.getMinutes();
        var second = currentDate.getSeconds() < 10 ? "0" + currentDate.getSeconds() : currentDate.getSeconds();

        var dateTimeTodayMidnight: string =
            currentDate.getFullYear() +
            "-" + month +
            "-" + day +
            "T" + "00" +
            ":" + "00" +
            ":" + "00";

        var dateTimeNow: string =
            currentDate.getFullYear() +
            "-" + month +
            "-" + day +
            "T" + hour +
            ":" + minute +
            ":" + second;

        const dateTimeFrom = ref<string>(dateTimeTodayMidnight);
        const dateTimeTo = ref<string>(dateTimeNow);
        //

        async function fetchUnitInfo() {
            const stationId = stationStore.selectedStation?.stationId;
            if (!stationId) {
                console.error("Station ID is not available.");
                return;
            }
            try {
                unitInfo.value = await getUnitInfo(stationId, props.unit.unitId, dateTimeFrom.value, dateTimeTo.value);
            } catch (error) {
                console.error("Failed to fetch unit info:", error);
            }
        }

        fetchUnitInfo();

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

        return {
            unitInfo,
            toggleUnitPopup,
            setSelectedUnit,
            backgroundColorCssClass,
            unitStateLabel
        };
    }
});
</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between bg-white-light">
        <div class="flex">
            <div class="text-3xl"> #{{ unit.unitId }} </div>
            <div class="ml-auto justify-end">
                <button @click="toggleUnitPopup(); setSelectedUnit(unit.value);">
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