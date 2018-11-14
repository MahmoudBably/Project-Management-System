$(document).ready(function(){
    
    var x = 0;
    $('#changed').hide();
    
    $('.view-collection').mouseover(function(){
        $('.view-collection').animate({
            'border-color' : 'transparent',
            'background-color' : '#283345',
        }, 300);
            
    });
    $("#modal-1").modal('show');
   

    
    $('.view-collection').mouseout(function(){
        $('.view-collection').animate({
            'border-color' : '#283345',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    $('.slide-down').mouseover(function(){
        $('.slide-down').animate({
            'border-color' : 'transparent',
            'background-color' : '#283345',
        }, 300);
            
    });
    
    
    $('.slide-down').mouseout(function(){
        $('.slide-down').animate({
            'border-color' : '#283345',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    $('.read-button').mouseover(function(){
        $('.read-button').animate({
            'border-color' : 'transparent',
            'background-color' : '#283345',
        }, 300);
            
    });
    
    
    $('.read-button').mouseout(function(){
        $('.read-button').animate({
            'border-color' : '#283345',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    
    
    $('.mail').mouseover(function(){
        $('.mail').animate({
            'border-color' : 'transparent',
            'background-color' : '#283345',
        }, 300);
            
    });
    
    
    $('.mail').mouseout(function(){
        $('.mail').animate({
            'border-color' : '#283345',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    
    
    $('.buy').mouseover(function(){
        $('.buy').animate({
            'border-color' : 'transparent',
            'background-color' : '#283345',
        }, 300);
            
    });
    
    
    $('.buy').mouseout(function(){
        $('.buy').animate({
            'border-color' : '#283345',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    
    
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
            $('#Bchanged').replaceWith('<div id="Bchanged" class="form-group has-error has-feedback in-position col-xs-3"><br /><br /><input type="email" style="text-align:left;" class="in-style subscribe input-style form-control" id="inputError" placeholder="Email" /><br /><br /><span style="margin-right:10px;" class="glyphicon glyphicon-remove form-control-feedback"></span></div>');
            $('.subscribe').val('');
        }
        
        else{
            $('#Bchanged').replaceWith('<div id="Bchanged" class="form-group has-success has-feedback in-position col-xs-3"><br /><br /><input type="text" style="text-align:left;" class=" subscribe form-control" id="inputSuccess"><br /><br /><span style="margin-right:10px;" class="glyphicon glyphicon-ok form-control-feedback"></span></div>');
        }
    });
    
    $(".slide-down").click(function() {
        $('html, body').animate({
            scrollTop: $(".subsection-one").offset().top
        }, 1000);
    });
    
    $(".slide-up").click(function() {
        $('html, body').animate({
            scrollTop: $(".secOne").offset().top
        }, 1000);
    });
    
});