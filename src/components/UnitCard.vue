<script setup lang="ts">
const props = defineProps<{
    state: string
}>()

var cssClass: String = "bg-yellow-warning";

switch (props.state) {
    case "AVAILABLE": cssClass = "bg-green-light";
        break
    case "IN USE": cssClass = "bg-red-light";
        break
    case "INACTIVE": cssClass = "bg-yellow-warning"
        break
}
</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between bg-violet-ultralight">
        <div class="flex">
            <div class="text-3xl">#1</div>
            <div class="ml-auto justify-end">
                <button>
                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset="" class="card-button-icon-small">
                </button>
            </div>
        </div>

        <div class="mt-2">
            <div class="grid grid-cols-2 text-sm">
                <div>Washes today:</div>
                <div class="font-bold">12</div>
                <div>Coin tray:</div>
                <div class="font-bold">69%</div>
            </div>
        </div>

        <div class="border-1 border-black rounded-lg text-center p-1" :class="cssClass">
            {{ state }}
        </div>
    </div>
</template>