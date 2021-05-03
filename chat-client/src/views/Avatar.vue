<template>
    <div class="container">
        <div class="avatar-form">
            <ErrorMessage  :message="error"/>
            <img :src="avatar"/>
            <label for="avatar">Avatar: </label>
            <input type="file" name="avatar" ref="file" v-on:change="handleFileUpload"/>    
            <button @click="submit">Upload avatar </button>
        </div>
    </div>
</template>

<script>
import ErrorMessage from '../components/ErrorMessage';
import { mapGetters,mapActions } from "vuex";

export default {
    components:{
        ErrorMessage
    },
    data(){
        return {
            file:'',
            error:''
        }
    },

    methods:{
        ...mapActions(["loadAvatar","setNewAvatar"]),
        async submit(){
            let formData = new FormData();
            formData.append('file', this.file);
            try{
                await this.setNewAvatar(formData);
                this.$router.push('/');
            }
            catch(e){
                 this.error=e.response.data.error;
            }
        },
        handleFileUpload(){
             this.file = this.$refs.file.files[0];
        }
    },
    computed: mapGetters(["avatar"]),
    created(){
        this.loadAvatar();
    }
}
</script>

<style scoped>
img{
    width: 300px;
    margin-bottom: 50px;
}
.container{
    height: 80vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
input{
    margin-bottom: 50px;
}
.avatar-form {

  width: 500px;
  align-items: center;
  justify-content: center;
  display: flex;
  flex-direction: column;
}
</style>