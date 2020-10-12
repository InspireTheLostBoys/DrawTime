<template>
    <div>   
      <v-toolbar color="primary" dense></v-toolbar>
        <v-container fluid class="fill-height">
            <v-row no-gutters>
                <v-col cols="12">
                    <h2 class="mt-2">9 pm promo draw</h2>
                    <div class="mt-2">502 tickets 23 persons</div>
                    <h4 class="mt-1">Prize value: R1750</h4>
                    <v-btn class="my-3" large block color="primary" @click="addEntry">Add Entry</v-btn>
                    <v-text-field append-icon="mdi-magnify" placeholder="Search..." dense hide-details outlined
                        class="mb-3">
                    </v-text-field>
                    <v-simple-table dense fixed-header height="calc(100vh - 285px)">
                        <template v-slot:default>
                            <thead>
                                <tr>
                                    <th class="text-left">
                                        Reference
                                    </th>
                                    <th class="text-left">
                                        Display
                                    </th>
                                    <th class="text-left">
                                        Tickets
                                    </th>
                                    <th class="text-left">
                                        Action
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in desserts" :key="item.name">
                                    <td>{{ item.reference }}</td>
                                    <td>{{ item.display }}</td>
                                    <td>{{ item.tickets }}</td>
                                    <td>
                                        <a href="#">Edit</a>
                                    </td>
                                </tr>
                            </tbody>
                        </template>
                    </v-simple-table>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>