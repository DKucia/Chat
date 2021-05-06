<template>
  <div>
      <div v-if="!toogle"> 
          <button @click="toogle=true">Create conversation</button>
      </div>
      <div v-else>
      <ErrorMessage :message="error"/>
     <input type="text" placeholder="username" v-model="username" />
     <input type="text" placeholder="name"  v-model="name"/>
     <button @click="submit">Create conversation</button> 
     <button @click="toogle=false" >Cancel</button>
     </div>
  </div>
</template>

<script>
import ErrorMessage from '../components/ErrorMessage';
import {mapActions} from 'vuex';

export default {
    name:"CreateConversationForm",
    components:{
        ErrorMessage
    },
    data(){
        return{
            toogle:false,
            error:'',
            name:'',
            username:''
        }
    },
    methods:{
        ...mapActions(["createConversation"]),
        async submit(){
            try{
                await this.createConversation({name:this.name,username:this.username});
                this.toogle=false;
            }catch(e){
                this.error=e.response.data.error;
            }
        }
    }
}
</script>

<style>

</style>