<template>
  <div>
    <v-layout row justify-center>
      <v-dialog v-model="dialog" persistent max-width="600">
        <v-card>
          <v-toolbar flat :color="type" dark>
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click.native="returnValue(false)">
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </v-toolbar>
          <!-- <v-card-title style="text-align: center; display: block;" class="headline"></v-card-title> -->
          <v-card-text class="mt-2" flat>
            <v-alert flat border="bottom" colored-border :type="type" elevation="2">{{description}}</v-alert>
          </v-card-text>
          <v-card-actions style="text-align: center; display: block;">
            <v-btn color="success darken-1" @click.native="returnValue(true)">Yes</v-btn>
            <v-btn color="error darken-1" @click.native="returnValue(false)">No</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-layout>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialog: false,
      afterRuturn: null,
      title: "",
      description: "",
      type: ""
    };
  },
  methods: {
    show(type, title, description, afterRuturn) {
      let self = this;
      self.type = type;
      self.title = title;
      self.description = description;
      self.afterRuturn = afterRuturn;
      self.dialog = true;
    },
    returnValue(value) {
      let self = this;
      self.afterRuturn(value);
      self.dialog = false;
    }
  }
};
</script>