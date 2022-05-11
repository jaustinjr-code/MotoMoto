<template>
  <div class="popup">
      <div class="popup-inner">
          <slot />
          <button class = "popup-close" @click = "TogglePopup()"> Close Popup</button>
      </div>
  </div>
</template>

<script>
export default {
    props: ['TogglePopup']
}
</script>

<style scoped>
.popup
{
    position: fixed;
    top:0; 
    left:0; 
    right:0; 
    bottom:0; 
    z-index: 99;
    background-color: rgba(0,0,0,0.2); 
    display: flex;
    align-content: center;
    justify-content: center;
}
.popup .popup-inner
{

    background: #FFF;
    padding: 32px;
    
}
</style>>
