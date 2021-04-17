<template>
  <div>
      <textarea placeholder="Type message..."  v-model="messageContent" ></textarea>
      <button @click="submit">Send</button>
  </div>
</template>

<script>
import {mapGetters} from 'vuex';

import signal from '../services/signal';
export default {
    name:'MessageInput',
    data(){
        return {
            messageContent:''
        }
    },
    computed:{
        ...mapGetters(['selectedConverationId']),
    },
    methods:{
        submit(){
            signal.connection.invoke('SendMessage',this.selectedConverationId,this.messageContent);
            this.messageContent='';
        }
    }
}
</script>

<style scoped>
    div{
        flex: 1;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    textarea{
        resize: none;
        padding: 0 15px;
        width: 60%;
        height: 50px;
        border-radius: 16px;
        box-shadow: none;
        font-size: 20px;
    }
    
    button{
        margin-left: 20px;
    }
</style>