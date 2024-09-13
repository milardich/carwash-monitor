<script setup lang="ts">
import { type CreateStationRequest, createStation } from '@/api/station.api';
import { createUnit } from '@/api/unit.api';
import { createWashCycle, type WashCycleRequest } from '@/api/washCycle.api';
import { reactive, ref } from 'vue';

// station
const stationRequest = reactive<CreateStationRequest>({
    stationName: '',
    city: '',
    streetName: '',
    streetNumber: '',
    country: ''
});

// for createUnit request
const unitRequestStationId = ref<number>();

// washcycle
const washCycleRequest = reactive<WashCycleRequest>({
    waterConsumption: 0.0,
    waxConsumption: 0.0,
    detergentConsumption: 0.0,
    coinAmount: 0
});
const washCycleRequestStationId = ref<number>();
const washCycleRequestUnitId = ref<number>();


</script>

<template>
    <div>
        <div class="p-4">
            <h1 class="text-3xl">Settings</h1>
        </div>
    </div>
    <br>

    <!-- CREATE STATION REQUEST -->
    <div class="flex space-x-4">
        <div class="h-full border rounded-lg p-4 content-small-container-color">
            <div class="text-xl">Create new station</div>
            <div class="mt-2">
                <div class="mt-2">
                    <input v-model="stationRequest.stationName" type="text" id="small-input" placeholder="Station name"
                        class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="stationRequest.city" type="text" id="small-input" placeholder="City"
                        class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="stationRequest.streetName" type="text" id="small-input" placeholder="Street"
                        class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="stationRequest.streetNumber" type="text" id="small-input"
                        placeholder="Street number" class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="stationRequest.country" type="text" id="small-input" placeholder="Country"
                        class="rounded-lg border p-1">
                </div>
            </div>
            <div class="mt-2">
                <button type="button" @click="createStation(stationRequest)"
                    class="w-full shadow-lg text-center p-2 rounded-lg border send-request-button">Send
                    request</button>
            </div>
        </div>


        <!-- CREATE UNIT REQUEST -->
        <div class="h-full border rounded-lg p-4 content-small-container-color">
            <div class="text-xl">Add new unit</div>
            <div class="mt-2">
                <div class="mt-2">
                    <input v-model="unitRequestStationId" type="text" id="small-input" placeholder="Station ID"
                        class="rounded-lg border p-1">
                </div>
            </div>
            <div class="mt-2 inset-x-0 bottom-0">
                <button type="button" @click="createUnit(unitRequestStationId)"
                    class="w-full shadow-lg text-center p-2 rounded-lg border send-request-button">Send
                    request</button>
            </div>
        </div>


        <!-- CREATE WASHCYCLE REQUEST -->
        <div class="h-full border rounded-lg p-4 content-small-container-color">
            <div class="text-xl">Create wash cycle</div>
            <div class="mt-2">
                <div class="mt-2">
                    <input v-model="washCycleRequestStationId" type="text" id="small-input" placeholder="Station ID"
                        class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="washCycleRequestUnitId" type="text" id="small-input" placeholder="Unit ID"
                        class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="washCycleRequest.waterConsumption" type="text" id="small-input"
                        placeholder="Water consumption" class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="washCycleRequest.waxConsumption" type="text" id="small-input"
                        placeholder="Wax consumption" class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="washCycleRequest.detergentConsumption" type="text" id="small-input"
                        placeholder="Detergent consumption" class="rounded-lg border p-1">
                </div>
                <div class="mt-2">
                    <input v-model="washCycleRequest.coinAmount" type="text" id="small-input" placeholder="Coin amount"
                        class="rounded-lg border p-1">
                </div>
            </div>
            <div class="mt-2 inset-x-0 bottom-0">
                <button type="button"
                    @click="createWashCycle(washCycleRequest, washCycleRequestStationId, washCycleRequestUnitId)"
                    class="w-full shadow-lg text-center p-2 rounded-lg border send-request-button">Send
                    request</button>
            </div>
        </div>

    </div>







    <!-- <div>
        <h2>Wash cycle -> POST: /api/station/{unitRequestStationId}/unit/{unitId}/wash-cycle</h2>
        <table>
            <tr>
                <td>
                    Water consumption:
                </td>
                <td>
                    <input v-model="message" placeholder="0.00" />
                </td>
            </tr>
            <tr>
                <td>
                    Detergent consumption:
                </td>
                <td>
                    <input v-model="message" placeholder="0.00" />
                </td>
            </tr>
            <tr>
                <td>
                    Wax consumption:
                </td>
                <td>
                    <input v-model="message" placeholder="0.00" />
                </td>
            </tr>
            <tr>
                <td>
                    Coin amount:
                </td>
                <td>
                    <input v-model="message" placeholder="5" />
                </td>
            </tr>
        </table>
        <button type="button" class="side-menu-item-selected">Add new wash cycle</button>
    </div> -->
</template>