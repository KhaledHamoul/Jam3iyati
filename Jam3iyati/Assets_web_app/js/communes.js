var communesDiv = document.getElementById('communes');
var rad = document.getElementsByName('typeAsso');

var apcs = function () {
    return `
            <div class ="mt-10">
                <div class ="form-control" id="default-select">
                    <select name="apc">
                        <option value="">Choisir</option>
                        <option value="1">apc 1</option>
                        <option value="2">apc 2</option>
                        <option value="3">apc 3</option>
                     </select>
                </div>
             </div>
              `;
}  



var prev = null;
for (i = 0; i < rad.length; i++) {
    rad[i].onclick = function () {
        alert('khaled');
        if (this !== prev) {
            prev = this;
            
            if (this.value == '1')
                communesDiv.innerHTML = "";
            else
                communesDiv.innerHTML = apcs;
        }

    };
}