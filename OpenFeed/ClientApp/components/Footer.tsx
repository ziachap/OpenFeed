import * as React from "react";

export class Footer extends React.Component<{}, {}> {
	render() {
		return <div className="ui inverted vertical footer segment">
			       <div className="ui center aligned container">
				       <div className="ui stackable inverted divided grid">
					       <div className="three wide column">
						       <h4 className="ui inverted header">Useful Links</h4>
						       <div className="ui inverted link list">
							       <a href="#" className="item">About Us</a>
							       <a href="#" className="item">Our Tech</a>
							       <a href="#" className="item">Updates</a>
						       </div>
					       </div>
					       <div className="seven wide left aligned  column">
						       <h4 className="ui inverted header">What do you think?</h4>
						       <p>Help us improve OpenFeed by giving us your feedback <a href="#">here</a></p>
					       </div>
				       </div>
				       <div className="ui inverted section divider"></div>
				       <img className="ui centered mini image" src="images/of_logo.png" alt="OpenFeed"/>
				       <div className="ui horizontal inverted small divided link list">
					       <a className="item" href="#">Site Map</a>
					       <a className="item" href="#">Contact Us</a>
					       <a className="item" href="#">Terms and Conditions</a>
					       <a className="item" href="#">Privacy Policy</a>
				       </div>
			       </div>
		       </div>;
	}
};