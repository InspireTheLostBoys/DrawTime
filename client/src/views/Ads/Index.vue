<template>
    <v-container>
        <h2 class="mt-2">Adverts</h2>
        <v-btn class="my-3" large block color="primary" @click="addAdvert">Add Advert</v-btn>

        <v-simple-table dense fixed-header height="calc(100vh - 285px)">
            <template v-slot:default>
                <thead>
                    <tr>
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
                        <td>{{ item.description }}</td>
                        <td>
                            <a @click="openEdit(item)">Edit</a>
                            <v-icon @click="deleteItem(item)" color="error"> mdi-delete </v-icon>
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
    export default {
        components: {
            Prompt,
            EditModal,
            YesNoModal
        },
        data() {
            return {
                playlists: []

            }
        },
        mounted() {
            let self = this
            self.getAll()
        },
        methods: {
            deleteItem(item) {
                let self = this
                console.log(item);
                self.$refs.YesNoModal.show("error", "Delete Advert", "Are you sure you want to delete this ad?",
                    afterRuturn => {
                        self.delete('dt_ads?id=' + item.id).then(r => {
                            self.getAll()
                        })
                    })
            },
            openEdit(item) {
                let self = this
                self.$refs.EditModal.open(item, cb => {
                    self.getAll()
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
                    self.post('dt_ads', newAd).then(r => {
                        self.getAll()
                        self.openEdit(r.data.dt_ad)
                    })

                })
            },
            getAll() {
                let self = this
                self.get(`dt_ads`).then(r => {
                    console.log(r);

                    self.playlists = r.data.dt_ads
                })
            }
        }
    }
</script>