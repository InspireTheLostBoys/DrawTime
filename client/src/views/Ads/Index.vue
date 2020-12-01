<template>
    <v-container>
        <h2 class="mt-2">Adverts</h2>
        <v-row>
            <v-col class="pa-1" cols="12" lg="6" md="6">
                <v-btn class="my-3" large block color="primary" @click="addAdvert">Add Advert</v-btn>
            </v-col>
        </v-row>

        <v-simple-table dense fixed-header height="calc(100vh - 285px)">
            <template v-slot:default>
                <thead>
                    <tr>
                        <th class="text-left">

                        </th>
                        <th class="text-left">
                            description
                        </th>
                        <th class="text-left">
                            Action
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in playlists" :key="item.description">
                        <td>
                            <v-card v-if="showImage" height="50" width="88">
                                <object height="50" width="88" :data="getImg('Ads',item)" type="image/png">
                                    <img height="100%" src="img/no-image.png" style="width:100% !important">
                                </object>
                            </v-card>
                        </td>
                        <td>{{ item.description }}</td>
                        <td>
                            <a @click="openEdit(item)">Edit</a>
                            <v-btn @click="deleteItem(item)" icon>
                                <v-icon color="error"> mdi-delete </v-icon>
                            </v-btn>
                        </td>
                    </tr>
                </tbody>
            </template>
        </v-simple-table>
        <Prompt ref="Prompt" />
        <EditModal ref="EditModal" />
        <YesNoModal ref="YesNoModal" />
    </v-container>
</template>
<script>
    import Prompt from "@/components/Prompt.vue"
    import EditModal from './EditModal.vue'
    import YesNoModal from "@/components/YesNoModal.vue"
    import axios from "axios"
    export default {
        components: {
            Prompt,
            EditModal,
            YesNoModal
        },
        data() {
            return {
                showImage: true,
                playlists: []
            }
        },
        mounted() {
            let self = this
            self.getAll()
        },
        methods: {
            getImg(directory, item) {
                let self = this
                return process.env.VUE_APP_IMAGE_SERVER_ADDRESS + "DrawTime/" + directory + '/' + item.id + ".png"
            },
            deleteItem(item) {
                let self = this
                console.log(item);
                self.$refs.YesNoModal.show("error", "Delete Advert", "Are you sure you want to delete this ad?",
                    afterRuturn => {
                        if (afterRuturn) {
                            self.delete('playlists?id=' + item.id).then(r => {
                                self.getAll()
                                axios.delete(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + 'delete/' +
                                        "Ads" + '/' + item.id)
                                    .then(r => {})
                            })
                        }

                    })
            },
            openEdit(item) {
                let self = this
                self.showImage = false
                self.$refs.EditModal.open(item, cb => {
                    self.getAll()
                    self.showImage = true
                })
            },
            addAdvert() {
                let self = this
                self.$refs.Prompt.show("info", "Please Enter a new advert name", "advert name", afterRuturn => {
                    let newAd = {
                        id: 0,
                        description: afterRuturn,
                        line1: "",
                        line2: "",
                        line3: "",
                        line4: "",
                        show_from: new Date(),
                        show_to: new Date(),
                    }
                    self.post('playlists', newAd).then(r => {
                        self.getAll()
                        self.openEdit(r.data.dt_ad)
                    })

                })
            },
            getAll() {
                let self = this
                self.showImage = false
                self.get(`playlists`).then(r => {
                    console.log(r);
                    self.playlists = r.data.dt_ads
                    self.showImage = true

                })
            }
        }
    }
</script>