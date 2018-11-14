$(document).ready(function(){ 
    
    $('.thanks-message').hide();
    
    $('.mail-button' || '.second-mail-button').mouseover(function(){
        $('.mail-button').animate({
            'border-color' : '#B99867',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    
    $('.mail-button' || '.second-mail-button').mouseout(function(){
        $('.mail-button').animate({
            'border-color' : 'transparent',
            'background-color' : '#B99867',
            color : '#283345'
        }, 300);
            
    });
    
    $('.second-mail-button').mouseover(function(){
        $('.second-mail-button').animate({
            'border-color' : '#B99867',
            'background-color' : 'transparent',
            color : '#B99867'
        }, 300);
            
    });
    
    
    $('.second-mail-button').mouseout(function(){
        $('.second-mail-button').animate({
            'border-color' : 'transparent',
            'background-color' : '#B99867',
            color : '#283345'
        }, 300);
            
    });
    
    $('#njs').click(function(){
        if($('#ejs').val()==0){
            $('#ejs').replaceWith('<div class="ejs form-group has-error has-feedback"><input type="email" style="text-align:left;" id="ejs" class="in-style input-style form-control" id="inputError" placeholder="Email" /></div>');
        }
        
        else{
            $('#modal-3').modal('hide');
            $('#modal-6').modal('show');
            $('#ejs').val("");
        }
    });
    
    $('#put').click(function(){
        
        if($('#inid1').val()==0){
            $('#inid1').replaceWith('<div class="form-group has-error has-feedback"><input type="text" style="text-align:left;" id="inid1" class="in-style input-style form-control" id="inputError" placeholder="Full Name" /><span style="margin-right:10px;" class="glyphicon glyphicon-remove form-control-feedback"></span></div>');
        }
        if($('#inid2').val()==0){
            $('#inid2').replaceWith('<div class="form-group has-error has-feedback"><input type="email" style="text-align:left;" id="inid2" class="in-style input-style form-control" id="inputError" placeholder="Email" /><span style="margin-right:10px;" class="glyphicon glyphicon-remove form-control-feedback"></span></div>');
        }
        
        if($('#inid3').val()==0){
            $('#inid3').animate({
            'border-color' : 'red',
        }, 300);
        }
        
        if($('#inid1').val()!=0 && $('#inid2').val()!=0 && $('#inid3').val()!=0){
            $('#modal-9').replaceWith('<div class="modal fade" id="modal-7"><div class="modal-dialog"><div class="modal-content"><div class="modal-body forget-style"><br />Thanks For Stopping By!... We Will Respond Very Soon.<br />We Appreciate Your Feedback.<br /></div><div class="modal-footer"><a href="" id="done" class="put-in btn btn-success" data-dismiss="modal">DONE</a></div></div></div></div>');
        }
        
        $('#done').click(function(){
        
            $('#modal-7').replaceWith('<div class="modal-content modal fade" id="modal-9"><div class="modal-header"><h3 class="modal-title">MAIL US</h3></div><div class="modal-body modal-body-style-1"><br    />We are Happy to See Your Message...<br /><br /><input id="inid1" class="in-style input-style form-control" type="text" placeholder="Full Name" /><br /><input id="inid2" class="in-style input-style          form-control" type="email" placeholder="Email" /><br /><textarea id="inid3" class="in-style input-style form-control" placeholder="Message" rows="6"></textarea> <br /><br /></div><div class="modal-       footer"><a href="#" class="put-in btn btn-primary btn-block" data-toggle="modal" data-target="#modal-7" data-dismiss="modal">SEND</a></div></div>');
            
        });
            
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
            $('#Bchanged').replaceWith('<div id="Bchanged" class="form-group has-error has-feedback in-position col-xs-3"><input type="email" style="text-align:left;" class="in-style subscribe input-style form-control" id="inputError" placeholder="Email" /><span style="margin-right:10px;" class="glyphicon glyphicon-remove form-control-feedback"></span></div>');
            $('.subscribe').val('');
        }
        
        else{
            $('#Bchanged').replaceWith('<div id="Bchanged" class="form-group has-success has-feedback in-position col-xs-3"><input type="text" style="text-align:left;" class=" subscribe form-control" id="inputSuccess"><span style="margin-right:10px;" class="glyphicon glyphicon-ok form-control-feedback"></span></div>');
        }
    });
    
});