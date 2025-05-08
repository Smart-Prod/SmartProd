document.getElementById('cep').addEventListener('blur', function () {
    const cep = this.value.replace(/\D/g, '');

    if (cep.length === 8) {
        fetch(`https://viacep.com.br/ws/${cep}/json/`)
            .then(response => response.json())
            .then(data => {
                if (!data.erro) {
                    document.getElementById('logradouro').value = data.logradouro;                    
                    document.getElementById('bairro').value = data.bairro;
                    document.getElementById('localidade').value = data.localidade;
                    document.getElementById('uf').value = data.uf;
                }
            });
    }
});