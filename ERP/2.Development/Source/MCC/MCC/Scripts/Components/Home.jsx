var homeStyle = {
    padding: '0 20px',
    borderRight: '1px dashed #E0E0E0'
}

var HomeComponent = React.createClass({
    getInitialState: function () {
        return { data: this.props.initialData };
    },
    render: function () {
        var commentNodes = this.state.data.map(function (comment, i) {
            return (
            <div key={i}>
                {comment.name}
            </div>
            );
        });
        return (
          <div className="row">
              <div className="col-md-7" style={homeStyle}>

              </div>
              <div className="col-md-5">
                  <div className="row text-center" style={{borderBottom: '1px solid #F5F5F5', marginTop: '15px', marginBottom: '10px'}}>
                    <div className="col-md-4 col-xs-4">
                        <a href="/Customer" className="icon-btn">
                            <i className="icon-users"></i>
                            <div> Customer </div>
                            <span className="badge badge-danger homeTotalCustomer"> 0 </span>
                        </a>
                    </div>
                    <div className="col-md-4 col-xs-4">
                        <a href="/ActivityManagement" className="icon-btn">
                            <i className="icon-calendar"></i>
                            <div> Working calendar </div>
                            <span className="badge badge-danger homeTotalActivity"> 0 </span>
                        </a>
                    </div>
                    <div className="col-md-4 col-xs-4">
                        <a href="/ApartmentBookingCardView" className="icon-btn">
                            <i className="icon-speedometer"></i>
                            <div> Transaction </div>
                            <span className="badge badge-danger homeTotalTransaction"> 0 </span>
                        </a>
                    </div>
                  </div>
              </div>
          </div>
      );
    }
});