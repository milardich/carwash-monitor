<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
</script>


<template>
    <div class="h-full">
        <div>
            <StationDropdown />
        </div>

        <div class="grid grid-cols-2 gap-4 h-percent-90 mt-5">
            <div class="rounded-lg overflow-y-auto p-6 h-full bg-white-light shadow-lg">
                <div class="text-3xl">Resource consumption</div>

                <!-- TODO: make this a component -->
                <div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-6 shadow-lg">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-6 shadow-lg">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-6 shadow-lg">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-6 shadow-lg">
                        Resource consumption graph 1
                    </div>
                </div>
            </div>

            <div class="rounded-lg overflow-y-auto p-6 h-full bg-white-light shadow-lg">
                <div class="text-3xl">Units</div>

                <!-- TODO: make this a component -->
                <div class="grid grid-cols-3 gap-4 mt-6">

                    <!-- unit #1 -->
                    <div
                        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between">
                        <div class="flex">
                            <div class="text-3xl">#1</div>
                            <div class="ml-auto justify-end">
                                <button>
                                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset=""
                                        class="card-button-icon-small">
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

                        <div class="border-1 border-black rounded-lg text-center bg-yellow-warning p-1">
                            INACTIVE
                        </div>
                    </div>

                    <!-- unit #1 -->
                    <div
                        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between">
                        <div class="flex">
                            <div class="text-3xl">#2</div>
                            <div class="ml-auto justify-end">
                                <button>
                                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset=""
                                        class="card-button-icon-small">
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

                        <div class="border-1 border-black rounded-lg text-center bg-green-light p-1">
                            AVAILABLE
                        </div>
                    </div>

                    <!-- unit #1 -->
                    <div
                        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between">
                        <div class="flex">
                            <div class="text-3xl">#3</div>
                            <div class="ml-auto justify-end">
                                <button>
                                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset=""
                                        class="card-button-icon-small">
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

                        <div class="border-1 border-black rounded-lg text-center bg-red-light p-1">
                            IN USE
                        </div>
                    </div>

                    <!-- unit #1 -->
                    <div
                        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-lg p-4 content-between">
                        <div class="flex">
                            <div class="text-3xl">#4</div>
                            <div class="ml-auto justify-end">
                                <button>
                                    <img src="@/assets/settings-svgrepo-com.svg" alt="" srcset=""
                                        class="card-button-icon-small">
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

                        <div class="border-1 border-black rounded-lg text-center bg-yellow-warning p-1">
                            INACTIVE
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>


</template>