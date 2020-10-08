<template>
  <v-app>
    <v-content>
      <v-img src="img/login.jpg" style="height: 100vh">
        <v-container fluid class="fill-height" style="background: transparent; height: 100vh">
          <v-row justify="center">
            <v-col cols="10" v-if="!reset" style="margin-top: 200px;">
              <v-form @submit.prevent="login" ref="form" v-model="valid" lazy-validation>
                <v-text-field prepend-inner-icon="mdi-account" dark dense placeholder="User ID" filled v-model="uid"
                  required>
                </v-text-field>

                <v-text-field type="password" prepend-inner-icon="mdi-lock" dark dense placeholder="Pin" filled v-model="pin" required>
                </v-text-field>

                <v-btn depressed rounded :loading="loading" type="submit" large block class="ml-0 mb-5" color="yellow">Sign in
                </v-btn>

                <div class="text-center">
                  <div class="error--text">{{ errorText }}</div>
                </div>
              </v-form>
            </v-col>
          </v-row>
        </v-container>
      </v-img>
    </v-content>
    <ErrorDialog ref="error" />
  </v-app>
</template>

<script>
  import Axios from "axios";
  import ErrorDialog from "../components/Common/Overlays/Dialogs/Error";

  export default {
    components: {
      ErrorDialog,
    },
    created() {},
    data: () => ({
      reset: false,
      loading: false,
      valid: true,
      uid: "",
      pin: "",
      errorText: "",
    }),
    methods: {
      login() {
        let self = this;

        self.errorText = '';

        if (this.$refs.form.validate()) {

          self.loading = true;

          setTimeout(() => {
            let request = {
              uid: self.uid,
              pin: self.pin
            };

            Axios.post(process.env.VUE_APP_API + "user/Login", request)
              .then((r) => {
                console.log(r.data);
                if (r.data.success) {
                  localStorage.setItem("userDetails", JSON.stringify({
                    user_id: r.data.user_id,
                    role_id: r.data.role_id
                  }));
                  self.$router.push("/Landing")
                } else {
                  self.errorText = r.data.clientError;
                  self.loading = false;
                }
              })
              .catch((e) => {
                let err = e.response.data;
                self.errorText = err.error_description;
                self.loading = false;
              });
          }, 500);
        }
      }
    },
  };
</script>