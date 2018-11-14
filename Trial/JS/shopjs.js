$(document).ready(function(){

    $('.subscribe-button').mouseover(function(){
        $('.subscribe-button').animate({
            'border-color' : '#B99867',
            'background-color' : 'transparent',
            color: '#B99867'
        }, 300);
            
    });
    
    
    $('.subscribe-button').mouseout(function(){
        $('.subscribe-button').animate({
            'border-color' : 'transparent',
            'background-color' : '#B99867',
            color : '#283345'
        }, 300);
            
    });
    
    $('.subscribe-button').click(function(){
        
        if($('.subscribe').val() == 0){
            $('#Bchanged').replaceWith('<div class="form-group has-error has-feedback in-position col-xs-3"><input type="email" class="in-style input-style form-control" id="inputError" placeholder="Email" /><span style="margin-right:10px;" class="glyphicon glyphicon-remove form-control-feedback"></span></div>');
        }
        
        else{
            $('#Bchanged').replaceWith('<div class="form-group has-success has-feedback in-position col-xs-3"><input type="text" class="form-control" id="inputSuccess"><span style="margin-right:10px;" class="glyphicon glyphicon-ok form-control-feedback"></span></div>');
        }
    });
    
    //------------------------------------------------------------------------------------
    
    $(".change-to1").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to1").html('REMOVE FROM CART');
            $(".change-to1").val('remove');
            $(".change-to1").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to1").html('ADD TO CART');
            $(".change-to1").val('add');
            $(".change-to1").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to2").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to2").html('REMOVE FROM CART');
            $(".change-to2").val('remove');
            $(".change-to2").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to2").html('ADD TO CART');
            $(".change-to2").val('add');
            $(".change-to2").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to3").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to3").html('REMOVE FROM CART');
            $(".change-to3").val('remove');
            $(".change-to3").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to3").html('ADD TO CART');
            $(".change-to3").val('add');
            $(".change-to3").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to4").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to4").html('REMOVE FROM CART');
            $(".change-to4").val('remove');
            $(".change-to4").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to4").html('ADD TO CART');
            $(".change-to4").val('add');
            $(".change-to4").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to5").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to5").html('REMOVE FROM CART');
            $(".change-to5").val('remove');
            $(".change-to5").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to5").html('ADD TO CART');
            $(".change-to5").val('add');
            $(".change-to5").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to6").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to6").html('REMOVE FROM CART');
            $(".change-to6").val('remove');
            $(".change-to6").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to6").html('ADD TO CART');
            $(".change-to6").val('add');
            $(".change-to6").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to7").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to7").html('REMOVE FROM CART');
            $(".change-to7").val('remove');
            $(".change-to7").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to7").html('ADD TO CART');
            $(".change-to7").val('add');
            $(".change-to7").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to8").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to8").html('REMOVE FROM CART');
            $(".change-to8").val('remove');
            $(".change-to8").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to8").html('ADD TO CART');
            $(".change-to8").val('add');
            $(".change-to8").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
    $(".change-to9").click(function() {
        if($(this).prop("value") == 'add'){
            $(".change-to9").html('REMOVE FROM CART');
            $(".change-to9").val('remove');
            $(".change-to9").removeClass("btn-success").addClass("btn-danger");
        }
        else{
            $(".change-to9").html('ADD TO CART');
            $(".change-to9").val('add');
            $(".change-to9").removeClass("btn-danger").addClass("btn-success");
        }
    });
    
});