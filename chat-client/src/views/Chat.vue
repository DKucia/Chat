<template>
  <div class="container">
    <ConversationsList />
    <MessageArea />
  </div>
</template>

<script>
import ConversationsList from '../components/ConversationsList';
import MessageArea from '../components/MessageArea';
import { mapGetters } from "vuex";
import signal from "../services/signal";

export default {
  components:{
    ConversationsList,
    MessageArea
  },
  computed: mapGetters(["getUsername"]),
  async created(){
    console.log("init");
    await signal.init(localStorage.getItem('token'));
  }
};
</script>

<style scoped>
  .container{
    display: flex;
    justify-content: left;
    flex-direction: row;
    height: 95%;
    margin: 15px 20px;
    width: 100%;
    border: 1px solid black;
  }
</style>