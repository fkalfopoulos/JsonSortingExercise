 
$(document).ready(function () {
    $('#Table').jtable({
        title: 'Json file',
        paging: true,
        pageSize: 10,
        sorting: true,
        

        actions: {
            listAction: '/Home/GetData',
             
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            userId: {
                title: 'userId',
                width: '15%'
            },         
            title: {
                title: 'title',
                width: '15%'
            },
            body: {
                title: 'body',
                width: '45%'
            }              
        }
    });
    $('#Table').jtable('load');
});
