
    var carasoulItem = function (i, carType , model) {
        return `<div class="carousel-item ${carType} ">
            <div>
              <h3>Membre ${(i + 1)} </h3>

              <h4>Nom</h4>
              ${model['lastName']}

              <h4>Prénom</h4>
              ${model['firstName']}

              <h4>Date de naissance</h4>
              ${model['birthday']}

              <h4>Lieu de naissance</h4>
              ${model['birthplace']}

              <h4>N° de la carte d'indentité</h4>
              ${model['cardId']}

            </div>
          </div> `;
    }

var carouselBody = function () {
    return `<h3>Fondateurs de l'associations </h3>
          <div id="carouselExampleFade" class="carousel slide carousel-fade" data-interval="999999" data-ride="false">
             <div id="membersCarousel" class="carousel-inner">
                
             </div>
             <a class="carousel-control-next" href="#carouselExampleFade" role="button" data-slide="next">
               <span class="carousel-control-next-icon" aria-hidden="true"></span>
               <span class="sr-only">Next</span>
             </a>
           </div>`;
}

    
dinfo = function (ele, param) {
    var id = ele.value;
    var element = document.getElementById('dinfo-card');
    $.get("http://localhost:14481/Association/getAssociationInfo?id=" + id, function (data, status) {
        element.innerHTML = `
                        <h2 class="align-self-center">Informations de la demande</h2>
                        <h4>Nom de l'association</h4>
                        ${data['associationName']}

                        <h4>Date de création</h4>
                        ${data['date']}

                        <h4>Domaine d'activité</h4>
                        ${data['field']}

                        <h4>ID de la demande</h4>
                        ${data['demandeID']}

                        <h4>Type de la demande</h4>
                        ${data['demandeType']}

                        <h4>Date de la demande</h4>
                        ${data['demandeDate']}
                        ` ;
        var href;
        var demID = data['demandeID'];
        var validBtn = 'Valider';
        switch (param) {
            case 'DACApc': href = '/demande/ValidateDacForApc?id=' + demID; break;
            case 'DAWApc': href = '/demande/ValidateDawForApc?id=' + demID; break;
            case 'DACDaira': href = '/demande/ValidateDacForDaira?id=' + demID; break;
            case 'DAWDaira': href = '/demande/ValidateDawForDaira?id=' + demID; break;
            case 'DACWilaya': href = '/demande/ValidateDacForWilaya?id=' + demID; break;
            case 'DAWWilaya': href = '/demande/ValidateDawForWilaya?id=' + demID; break;
            case 'DACNotify': href = '/demande/DACNotify?id=' + demID; validBtn = 'Notifier'; break;
            case 'DAWNotify': href = '/demande/DAWNotify?id=' + demID; validBtn = 'Notifier'; break;
        }

        var validBtn = "";
        if ((param != 'archives')&&(param != 'validated' )) validBtn = `<a href="${href}"><button class ="btn btn-primary"><i class ="fa fa-check"></i> &nbsp ${validBtn}</button></a>`;
        element.innerHTML += carouselBody() + `
                                <br><br>
                                <div class="float-right">
                                      ${validBtn}
                                      <button onclick="PrintElem('toPrint')" class ="btn btn-primary"><i class ="fa fa-file-alt"></i></button>
                                </div>
                        `;
        var mc = document.getElementById('membersCarousel');
        for (var i = 0; i < data['members'].length; i++) (i == 0) ? mc.innerHTML += carasoulItem(i, 'active', data['members'][i]) : mc.innerHTML += carasoulItem(i, '', data['members'][i]);
                    
        console.log(data);
    });

}

dvalidate = function (element,param) {
    var id = element.value;
    var element = document.getElementById('dvalidate-card');
    $.get("http://localhost:14481/Association/getAssociationInfo?id=" + id, function (data, status) {

        var href;
        var demID = data['demandeID'];
        var validBtn = 'Valider';
        switch (param) {
            case 'DACApc': href = '/demande/ValidateDacForApc?id=' + demID; break;
            case 'DAWApc': href = '/demande/ValidateDawForApc?id=' + demID; break;
            case 'DACDaira': href = '/demande/ValidateDacForDaira?id=' + demID; break;
            case 'DAWDaira': href = '/demande/ValidateDawForDaira?id=' + demID; break;
            case 'DACWilaya': href = '/demande/ValidateDacForWilaya?id=' + demID; break;
            case 'DAWWilaya': href = '/demande/ValidateDawForWilaya?id=' + demID; break;
            case 'DACNotify': href = '/demande/DACNotify?id=' + demID; validBtn = 'Notifier'; break;
            case 'DAWNotify': href = '/demande/DAWNotify?id=' + demID; validBtn = 'Notifier'; break;
        }
        element.innerHTML = `
                            <h2 class ="align-self-center">Valider la demande</h2>
                            Vouler-vous valider cette demande ?  <b> ID: ${data['demandeID']}  </b>
                            <br><br>
                            <div class ="float-right">
                                <a href="${href}"><button class ="btn btn-primary"><i class ="fa fa-check"></i> &nbsp ${validBtn}</button></a>
                                <button class ="btn btn-primary"><i class ="fa fa-file-alt"></i></button>
                            </div>
                        `;
    });
}

dcancel = function (ele,param) {
    var id = ele.value;
    var element = document.getElementById('dcancel-card');
    $.get("http://localhost:14481/Association/getAssociationInfo?id=" + id, function (data, status) {

        var href;
        var demID = data['demandeID'];
        switch (param) {
            case 'DACApc': href = '/demande/CancelDac?id=' + demID; break;
            case 'DAWApc': href = '/demande/CancelDawForApc?id=' + demID; break;
            case 'DACDaira': href = '/demande/CancelDacDaira?id=' + demID; break;
            case 'DAWDaira': href = '/demande/CancelDawForDaira?id=' + demID; break;
            case 'DACWilaya': href = '/demande/CancelDacForWilaya?id=' + demID; break;
            case 'DAWWilaya': href = '/demande/CancelDaw?id=' + demID; break;
        }

        element.innerHTML = `
                            <h2 class ="align-self-center">Confirmer l'annulation</h2>
                            Voulez-vous vraiment annuler cette demande ? <b> ID : ${data['demandeID']} </b>
                            <br><br>
                            <form action="${href}" method="post">
                            
                                <div class ="form-group">
                                    <h4>Remarque</h4>
                                    <input type="text" name="remarque" class ="form-control" placeholder="Remarque">
                                </div>
                                <br><br>
                                <div class ="float-right">
                                     <button type="submit" class ="btn btn-danger"><i class ="fa fa-times"></i> &nbsp Confirmer</button> </a>
                                </div>
                            </form>
                        `;
    });
}
