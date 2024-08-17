<script setup lang="ts">
import { ref } from 'vue';

const message = ref<string>("");
</script>

<template>
    <div>
        <h1 class="text-3xl font-bold underline">Settings view</h1>
        <div>TODO: this view is for creating new stations, units and wash cycles.</div>
        <br>
        <div>NOTE: not implemented yet</div>
    </div>
    <br>
    <div>
        <h2>Wash cycle -> POST: /api/station/{stationId}/unit/{unitId}/wash-cycle</h2>
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
    </div>
</template>