var rad = document.getElementsByName('typeAsso');
var car = document.getElementById('car');
var communesDiv = document.getElementById('communes');

var carasoulItem = function(i,carType){
  return `<div class="carousel-item ${carType}">
            <div class="col-lg-5" style="margin:auto;margin-top:100px;margin-bottom:100px">
              <h4>Membre ${(i+1)} </h4>
              <br>
              <div class="mt-10 ">
                <input  type="text" name="firstName${i}" placeholder="Prenom" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Prenom'" required class ="form-control">
              </div>
              <div class="mt-10">
                <input  type="text" name="lastName${i}" placeholder="Nom" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Nom'" required class ="form-control">
              </div>
              <br>
              <div class ="mt-10">
                <h5>Date de naaissance</h5>
                <input  type="date" name="birthday${i}" placeholder="Date de naissance" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Date de naissance'" required class ="form-control">
              </div>
              <div class ="mt-10">
                <input type="text" name="birthplace${i}" placeholder="Lieu de naissance" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Lieu de naissance'" required class ="form-control">
              </div>
              <div class ="mt-10">
                <input type="text" name="cardId${i}" placeholder="numero de la carte d'indentite" onfocus="this.placeholder = ''" onblur= "this.placeholder = `+"`numero de la carte d'indentite`" + ` " required class ="form-control">
              </div>
            </div>
          </div> `;
        }

var carouselBody = function(){
  return `<h3>Fondateurs de l'associations </h3>
          <div id="carouselExampleFade" class="carousel slide carousel-fade" data-interval="999999" data-ride="false">
             <div id="membersCarousel" class="carousel-inner">

             </div>
             <a class="carousel-control-prev" href="#carouselExampleFade" role="button" data-slide="prev">
               <span class="carousel-control-prev-icon" aria-hidden="true"></span>
               <span class="sr-only">Previous</span>
             </a>
             <a class="carousel-control-next" href="#carouselExampleFade" role="button" data-slide="next">
               <span class="carousel-control-next-icon" aria-hidden="true"></span>
               <span class="sr-only">Next</span>
             </a>
           </div>`;
}


var getApcs = function () {
    var apcSelectElement = document.getElementsByName('apc');
    var wilayaSelectElement = document.getElementsByName('wilaya');

    
}


var prev = null;
for( i = 0; i < rad.length; i++) {
    rad[i].onclick = function() {

        if (prev == null) car.innerHTML =  carouselBody();
        
        if(this !== prev) {
            prev = this;
            var mc = document.getElementById('membersCarousel');
            mc.innerHTML = '';
            if(this.value == '1')  {
                for (var i = 0; i < 15; i++) (i == 0) ? mc.innerHTML += carasoulItem(i, 'active') : mc.innerHTML += carasoulItem(i, '');
                communesDiv.innerHTML = "";
            }
            
            else {
                for (var i = 0; i < 10; i++) (i == 0) ? mc.innerHTML += carasoulItem(i, 'active') : mc.innerHTML += carasoulItem(i, '');
                $("#communes").load("http://localhost:14481/apc/getApcs?id=14");
            }
              
        }

    };
}
