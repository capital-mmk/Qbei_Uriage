<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Entry.aspx.cs" Inherits="Qbei_Uriage.UserList.User_Entry" MasterPageFile="~/Uriage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/validate.js"></script>
        <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.0/jquery.validate.min.js"></script>--%>
    <script src="../js/jquery.formtowizard.js"></script>
    <script src="../js/jquery.formtowizard.js"></script>
    <script src="../js/jquery.min.js"></script>
    <link href="../css/register.css" rel="stylesheet" />
<%--    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt" crossorigin="anonymous">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>--%>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
     <style>
        .panel-custom1 > .panel-heading {
    color: white;
    background-color: #008080 !important;
    border-color: #008080 !important;
    text-align: center !important;
    font-size: 16px;
}

.panel-heading {
    padding: 10px 15px !important;
    border-bottom: 1px solid transparent !important;
    border-top-left-radius: 3px !important;
    border-top-right-radius: 3px !important;
}
       
       

        .panel-Search > .panel-heading {
            color: white !important;
            background-color: #395587 !important;
            border-color: #395587 !important;
            text-align: center !important;
            font-size: 16px;
        } 

        .panel-heading {
            padding: 10px 15px !important;
            border-bottom: 1px solid transparent !important;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
				<div class="panel panel-custom1" style="margin-top:-40px;" onload="myFunction()"><div class="panel-heading" style="width:100%"><span class="glyphicon glyphicon-file"></span>ユーザー設定</div>
		     </div> 
    <div class="container">
        
			<div class="row main">
				<div class="main-login main-center" style="max-width:800px !Important; margin-top:-80px; border-radius:10px;">
				     <div style="background-color:#395587;    height: 40px; vertical-align:middle; line-height:40px;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    margin-top: -9px;
    background-color: #395587;margin-left: -5.5%;
    width: 111%;" align="center">登録
		</div>
						<form  id="form1">
						<div class="form-group">
							<label for="name" class="cols-sm-2 control-label">ユーザー名</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" class="form-control required" runat="server" name="name" id="txtUserName"/>
								</div>
							</div>
						</div>

                    	<div class="form-group" runat="server" id="old_pass_div">
							<label for="username" class="cols-sm-2 control-label" runat="server" id="change_name"> パスワード</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
									<input  class="form-control" name="username" runat="server" id="txtpassword"  />
								</div>
							</div>
						</div>

						<div class="form-group" runat="server" id="new_hide" visible="false">
							<label for="email" class="cols-sm-2 control-label">新しいパスワード</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
									<input runat="server" type="password" class="form-control" name="email" id="new_pass"  placeholder="Enter your Password"/>
								</div>
							</div>
						</div>

					

						<div class="form-group" runat="server" id="con_hide" visible="false">
							<label for="password" class="cols-sm-2 control-label" >パスワードの確認入力</label>
							<div class="cols-sm-10">
								<div class="input-group ">
									<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
									<input type="password" runat="server" class="form-control" name="password" id="comfirm_pass"  placeholder="Enter your Confirm Password"/>
								</div>
							</div>
						</div>

						<div class="form-group" runat="server" style="display:none;">
							<label for="confirm" class="cols-sm-2 control-label">更新日</label>
							<div class="cols-sm-10 ">
								<div class="input-group ">
									<span class="input-group-addon "><i class="glyphicon glyphicon-calendar" aria-hidden="true"></i></span>
									<input type="text" runat="server" onkeydown = "return DateFormat(this, event.keyCode)"  onblur="myFunction()" class="form-control form_date" name="confirm" id="txtModifiedDate"  placeholder="yyyy/mm/dd"/>
								</div>
							</div>
						</div>
                            </form>
						<div class="form-group ">
							<%--<button  runat="server"  id="btnsave" value="Save"  class="" />--%>
                            <input type="button" id="btnsave"  runat="server"   class="btn btn-primary btn-lg btn-block login-button" value="登録" onserverclick="Save_Click" />
						</div>
						
				
				</div>
			</div>
		</div>
      <script type="text/javascript">
          function isNumber(evt) {
              evt = (evt) ? evt : window.event;
              var charCode = (evt.which) ? evt.which : evt.keyCode;
              if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                  return false;
              }
              return true;
          }
          var isShift = false;
          var seperator = "/";
          function DateFormat(txt, keyCode) {
              if (keyCode == 16)
                  isShift = true;
              //Validate that its Numeric
              if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false && keyCode != 32) {
                  if ((txt.value.length == 4 || txt.value.length == 7) && keyCode != 8) {
                      txt.value += seperator;
                  }
                  return true;

              }
              else {
                  return false;

              }
          }

          $(document).ready(function () {
              document.getElementById("<%= txtModifiedDate.ClientID%>").onfocusout = function () { myFunction() };
            function myFunction() {
                var fwo = document.getElementById("<%= txtModifiedDate.ClientID %>");
                var bobo = fwo.value.toString();
                var lastPart1 = bobo.split("/").pop();
                var yy = bobo.split("/")[1];
                var zz = bobo.split("/")[0];
                var x = parseInt(lastPart1, 10);
                var y = parseInt(yy, 10);
                var z = parseInt(zz, 10);
                if (z > 2000 && z < 2100 && y > 0 && y < 13 && x > 0 && x < 32 && (fwo.value.length) > 7 && (fwo.value.length) < 11) {
                    return true;
                }
                else {
                    fwo.value = "";
                }
            }
        });
    </script>
    <script type = "text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false && keyCode != 32) {
                if ((txt.value.length == 4 || txt.value.length == 7) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;

            }
            else {
                return false;

            }
        }
     
    </script>
	<script>
	    $(document).ready(function () {
	        $("#form1").validate(
                {
                
	            rules: {
	                name: "required"
	            },
	            messages: {
	                name: "Please specify your name"

	            }
	        })
            
	        $('#btnsave').click(function () {
	            alert("ji");
	            $("#form1").valid();
	        });
	    });
	</script>


    <script>
        $(function () {
            var $signupForm = $('#SignupForm');

            $signupForm.validate({ errorElement: 'em' });

            $signupForm.formToWizard({
                submitButton: 'SaveAccount',
                nextBtnClass: 'btn btn-primary next',
                prevBtnClass: 'btn btn-default prev',
                buttonTag: 'button',
                validateBeforeNext: function (form, step) {
                    var stepIsValid = true;
                    var validator = form.validate();
                    $(':input', step).each(function (index) {
                        var xy = validator.element(this);
                        stepIsValid = stepIsValid && (typeof xy == 'undefined' || xy);
                    });
                    return stepIsValid;
                },
                progress: function (i, count) {
                    $('#progress-complete').width('' + (i / count * 100) + '%');
                }
            });
        });
    </script>

       <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" />
        <script src="../js/bootstrap-datetimepicker.js"></script>
           <script type="text/javascript">

               $('.form_date').datetimepicker(

                   {
                       language: 'es',
                       format: 'yyyy/mm/dd',
                       clearBtn: 1,
                       autoclose: 1,
                       weekStart: 1,
                       startView: 2,
                       todayBtn: 1,
                       forceParse: 0,
                       minView: 2,
                       pickerPosition: "top-right"

                   });
    </script>
    <script src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        function myFunction() {
            //simple example 1
            //click event for first button
            //$("#form1").validate();
            //alert('hi');
            //$('#btnsave').click(function ()
            //{
            //alert("f");
            document.getElementById('form1').valid(); //validate form 1
            //});
        }
        </script>
 </asp:Content>
