<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LookUp.ascx.vb" Inherits="Lookup_Web_Control.LookUp" %>
<div id="LookUpContainer">
    <span>
        <asp:TextBox id="txtLookUpKey" name="LookUpKey" runat="server"  class="text ui-widget-content ui-corner-all" Width="3em" />
    </span>
    <span>
        <input runat="server" id="LookUpButton" style="background:url(../Styles/images/3.png) no-repeat;background-size:22px 22px;
                width:20px;height:20px;border:none;text-indent: -10000px;cursor:pointer" title="Select" />
    </span>
    <span>
        <asp:TextBox id="txtLookUpDescription" name="LookUpDescription" runat="server" class="text ui-widget-content ui-corner-all" EnableViewState="true" />
    </span>
    <div id="LookUpPopUp" title="Select" runat="server">
        <div style="padding-bottom:3px;">
            <div>
                Search : <asp:TextBox id="txtLookUpSearch" name="LookUpSearch" style="vertical-align:middle" runat="server" 
                    class="text ui-widget-content ui-corner-all" Width="13.5em" />
                <asp:Button id="btnsearch" runat="server" Text="Search" style="line-height:1em;"  />
            </div>
        </div>
        <div id="LookUpTable" runat="server" class="mb-LookUp"><asp:Table ID="LookUpData" runat="server"></asp:Table></div>
        <div id="PageNavPosition" runat="server" class="mb-PageNav">
            <a href="#">1</a> <a href="#">2</a> <a href="#">3</a> <a href="#">4</a> <a href="#">5</a>
        </div>
    </div>
</div>
<script type="text/javascript">
    $('#' + '<%= Me.LookUpButton.ClientID%>').focus(function () {
        $('#' + '<%= Me.LookUpButton.ClientID%>').focusout();
    });
   
    $('#' + '<%= Me.txtLookUpKey.ClientID%>').change(function () {
        $rows = $('#' + '<%= Me.LookUpData.ClientID%>').find('tr:not(:has(th))')
         $row = $rows.find("td").filter(function () {
             return $(this).text() == $('#' + '<%= Me.txtLookUpKey.ClientID %>').val();
         }).closest("tr")
        var DescriptionValue;
        if ($row.length > 0) {
            $cols = $row.find('td');
            DescriptionValue = $cols[1].innerHTML;
            for (var i = 2; i < $cols.length; i++) {
                DescriptionValue += " " + $cols[i].innerHTML;
            }
        }
        else {
            var pos, h, w;
            DescriptionValue = "";
            //$('#' + '<%= Me.txtLookUpKey.ClientID%>').val('');
            if ('<%= Me.ShowDescription%>' == 'True') {
                pos = $('#' + '<%= Me.txtLookUpDescription.ClientID%>').offset();
                h = $('#' + '<%= Me.txtLookUpDescription.ClientID%>').height();
                w = $('#' + '<%= Me.txtLookUpDescription.ClientID%>').width();
                 
            }
            else {
                pos = $('#' + '<%= Me.LookUpButton.ClientID%>').offset();
                h = $('#' + '<%= Me.LookUpButton.ClientID%>').height();
                w = $('#' + '<%= Me.LookUpButton.ClientID%>').width();
            }
           
            $("<div class='custom-error'>Invalid Lookup Value!</div>").appendTo("body")
               .css({ left: pos.left + w + 10, top: pos.top - 2 }).fadeOut(3000);
            $('#' + '<%= Me.txtLookUpKey.ClientID%>').focus();
 
            
        }
        
        $('#' + '<%= Me.txtLookUpDescription.ClientID%>').val(DescriptionValue);
        $('#' + '<%= Me.GetDescriptionControl.ClientID%>').val(DescriptionValue);
     });
    $('#' + '<%= Me.btnsearch.ClientID%>').click(function () {
        var val = $.trim($('#' + '<%= Me.txtLookUpSearch.ClientID %>').val()).replace(/ +/g, ' ').toLowerCase();
        if (val.length == 0) {
            LookUpPager.isFilter = 0;
            LookUpPager.init();
            LookUpPager.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'));
            LookUpPager.showPage(1);
        }
        else {
            LookUpPager.isFilter = 1;
            $rows.show().filter(function () {
                var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                return !~text.indexOf(val);
            }).hide();
            LookUpPager.init();
            LookUpPager.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'));
                LookUpPager.showPage(1);
            }
    });
    //$(function () { $('#' + '<%= Me.txtLookUpDescription.ClientID%>').attr("disabled", true)})
    $('#' + '<%= Me.LookUpPopUp.ClientID %>').dialog({ autoOpen: false, width: 'auto', modal: true, title: '<%= Me.PopUpTitle %>', beforeClose: function (event, ui) { var element = document.getElementById('<%= Me.PageNavPosition.ClientID%>'); element.innerHTML = ""; } });
    $('#' + '<%= Me.LookUpButton.ClientID %>').click(function () {
        $('#' + '<%= Me.txtLookUpSearch.ClientID %>').val('');
        LookUpPager = new Pager('<%= Me.LookUpData.ClientID%>', parseInt('<%= Me.PageSize%>'));
        $rows = $('#' + '<%= Me.LookUpData.ClientID%>').find('tr:not(:has(th))')
        LookUpPager.init();
        LookUpPager.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'));
        LookUpPager.showPage(1);
        $('#' + '<%= Me.LookUpPopUp.ClientID %>').dialog('open'); return false;
    });
    var VisibleRows, LookUpPager, $rows;
    $('#' + '<%= Me.txtLookUpSearch.ClientID %>').keyup(function () { var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
        if (val.length == 0) {
            LookUpPager.isFilter = 0;
            LookUpPager.init();
            LookUpPager.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'));
            LookUpPager.showPage(1);
        }
        else {
            LookUpPager.isFilter = 1;
            $rows.show().filter(function () {
                var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                return !~text.indexOf(val);
            }).hide();
            LookUpPager.init();
            LookUpPager.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'));
            LookUpPager.showPage(1);
        }
    });
    var table = document.getElementById('<%= Me.LookUpData.ClientID%>');
    var tbody = table.getElementsByTagName("tbody")[0];
    tbody.onclick = function (e) {
        e = e || window.event;
        var data = [];
        var target = e.srcElement || e.target;
        while (target && target.nodeName !== "TR") { target = target.parentNode; }
        if (target) { var cells = target.getElementsByTagName("td"); for (var i = 0; i < cells.length; i++) { data.push(cells[i].innerHTML); }}
        $('#' + '<%= Me.txtLookUpKey.ClientID %>').val(data[0]);
        var DescriptionValue = "";
        DescriptionValue = data[1];
        var DescCols = parseInt('<%= Me.DescriptionColumnName.Split(",").Length%>');
        for (var col = 2; col <= DescCols; col++) {
                DescriptionValue += " " + data[col];
            }
       $('#' + '<%= Me.txtLookUpDescription.ClientID%>').val(DescriptionValue);
       $('#' + '<%= Me.GetDescriptionControl.ClientID%>').val(DescriptionValue);
        $('#' + '<%= Me.LookUpPopUp.ClientID %>').dialog('close');
        
    };
    function Pager(tableName, itemsPerPage) {
        this.tableName = tableName;
        this.itemsPerPage = itemsPerPage;
        this.pagerPage = 0;
        this.pagerPerPage = 5;
        this.currentPage = 1;
        this.pages = 0;
        this.inited = false;
        this.isFilter = 0;
        this.showRecords = function (from, to) {
            if (this.isFilter == 0) { var rows = document.getElementById(tableName).rows; }
            else { var rows = VisibleRows; }
            for (var i = 1; i < rows.length; i++) { if (i < from || i > to) { rows[i].style.display = 'none'; } else { rows[i].style.display = ''; }}
        }
        this.init = function () {
            if (this.isFilter == 0) { var rows = document.getElementById(tableName).rows; }
            else { var rows = $('#' + tableName + ' tr:visible'); VisibleRows = rows; }
            var records = (rows.length - 1);
            this.pages = Math.ceil(records / itemsPerPage);
            this.pagerPage = 0;
            this.inited = true;
        }
        this.showPageNav = function (pagerName, positionId, itemsPerPage) {
            if (!this.inited) { alert("not inited"); return; }
            var element = document.getElementById(positionId);
            var pagerHtml;// = '<span onclick="' + pagerName + '.prev();" class="pg-normal"> &#171 Prev </span> | ';
            pagerHtml = '<span> No Data Found. </span>';
            if (this.pages > 1) { pagerHtml = '<span onclick="' + pagerName + '.prev();" class="pg-normal"> &#171 Prev </span> | '; }
            if (this.pages > 1) {
                var from = (this.pagerPage) * this.pagerPerPage + 1;
                var to = from + this.pagerPerPage - 1;
                if (to > this.pages) { to = this.pages; }
                for (var page = from; page <= to; page++) {
                    pagerHtml += '<span id="pg' + page + '" class="pg-normal" onclick="' + pagerName + '.showPage(' + page + ');">' + page + '</span> | ';
                }
            }
            else {
                for (var page = 1; page <= this.pages; page++) {
                    pagerHtml = '<span id="pg' + page + '" class="pg-normal" onclick="' + pagerName + '.showPage(' + page + ');">' + page + '</span>';
                }
            }
            
            if (this.pages > 1) { pagerHtml += '<span onclick="' + pagerName + '.next();" class="pg-normal"> Next &#187;</span>'; }
            
            element.innerHTML = pagerHtml;
        }
        this.prev = function () {
            if (this.currentPage > 1) {
                if (this.currentPage == (this.pagerPage * this.pagerPerPage + 1)) {
                    this.pagerPage -= 1;
                    this.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'))
                }
                this.showPage(this.currentPage - 1);
            }
        }
        this.next = function () {
            if (this.currentPage < this.pages) {
                if (this.currentPage == ((this.pagerPage + 1) * this.pagerPerPage)) {
                    this.pagerPage += 1;
                    this.showPageNav('LookUpPager', '<%= Me.PageNavPosition.ClientID%>', parseInt('<%= Me.PageSize%>'));
                }
                this.showPage(this.currentPage + 1);
                
            }
        }
        this.showPage = function (pageNumber) {
            if (!this.inited) { alert("not inited"); return; }
            var oldPageAnchor = document.getElementById('pg' + this.currentPage);
            if (oldPageAnchor != null) oldPageAnchor.className = 'pg-normal';
            this.currentPage = pageNumber;
            var newPageAnchor = document.getElementById('pg' + this.currentPage);
            if (newPageAnchor != null) newPageAnchor.className = 'pg-selected';
            var from = (pageNumber - 1) * itemsPerPage + 1;
            var to = from + itemsPerPage - 1;
            this.showRecords(from, to);
        }
    }
</script>
<style>

</style>