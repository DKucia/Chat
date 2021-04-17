<template>
  <div> 
      <Message :message="message" v-for="message in messages" :key="message.id"/>

    </div>
</template>

<script>
import Message from './Message';
import {mapGetters,mapActions} from 'vuex';
import signal from '../services/signal';

export default {
    name:'MessageList',
    components:{
        Message
    },
    computed:{
        ...mapGetters(['messages'])
    },
    methods:{
        ...mapActions(['addMessage']),
        addNewMessage(message){
            this.addMessage(message);
        }
    },
    created(){
        signal.connection.on('ReceiveMessage',this.addNewMessage);
    }
}
</script>

<style scoped>
    div{
        flex: 10;
    }
</style>