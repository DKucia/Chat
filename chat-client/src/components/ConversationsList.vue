<template>
  <div class="conversations" >
        <CreateConversationForm/>
        <ConversationItem  :conversation="conversation" v-for="conversation in conversations" :key="conversation.id"/>
      </div>
</template>

<script>
import ConversationItem from '../components/ConversationItem';
import CreateConversationForm from '../components/CreateConversationForm';
import signal from '../services/signal';
import {mapActions,mapGetters} from 'vuex';

export default {
    components:{
        ConversationItem,
        CreateConversationForm
    },
    name:'ConversationsList',
    methods:{
        ...mapActions(['loadConversations','selectConversation','addNewConversation']),

        addConversation(conversation){
            console.log(conversation);
            this.addNewConversation(conversation);
        }
    },
    computed:{
        ...mapGetters(['conversations']),
    },
    async created(){
        await this.loadConversations();
        signal.connection.on('NewConversation',this.addConversation);
    }
}
</script>

<style scoped>
    .conversations{
        box-sizing: border-box;
        padding: 15px;
        width: 25%;
        display: flex;
        flex-direction: column;
        height: 100%;
        border: 1px solid grey;
    }
</style>