import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { connect } from 'react-redux';
import * as NewsReelState from "../store/NewsReel";
import { ApplicationState } from '../store';
import ArticleCard from "./ArticleCard";
import { newsCategories, newsCategoryService} from "../models/NewsCategories";
import "jquery";

type NewsReelProps = NewsReelState.NewsReelState
    & typeof NewsReelState.actionCreators
    & RouteComponentProps<{}>;

class NewsReel extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
		this.setCategoryHandler = this.setCategoryHandler.bind(this);
		this.renderCategoryInput = this.renderCategoryInput.bind(this);
	}

    componentWillMount() {
	    this.requestArticles();
    }

    componentWillReceiveProps(nextProps: NewsReelProps) {
	}

	private requestArticles(): void {
		this.props.requestArticles(this.props.searchConfiguration);
	}

	public setCategoryHandler(event: any, category?: number): void {
		this.props.setCategory(category);
		this.requestArticles();
	}

	public setPageHandler(event: any, page: number): void {
		this.props.setPage(page);
		this.requestArticles();
	}

    public render() {
        return <div className="ui container text">

                   <div className="ui very padded segment">
                       <div className="ui right rail">
                           {this.renderMenu()}
                       </div>
				<h1>United Kingdom > {newsCategoryService.getName(this.props.searchConfiguration.categoryId)}</h1>
				<p>This is the news!</p>
	                   {this.renderPagination()}
				{this.props.isLoading ? <span>Loading...</span> : this.renderArticles()}
				{this.renderPagination()}
                   </div>
               </div>;
	}

    private renderArticles() {
        return <div className="ui divided items">
			{this.props.paginatedArticles.results.map(article => <ArticleCard {...article} key={article.title}/>)}
               </div>;
	}

	// TODO: This should be contained within a reusable component
	private renderPagination() {
		return <div className="ui pagination menu">
			       {this.renderPaginationInput("<",
				       this.props.paginatedArticles.page - 1,
				       false,
				       !this.props.paginatedArticles.hasPreviousPage)}
			       {
				       Array.from(Array(this.props.paginatedArticles.totalPages).keys())
					       .map(i => this.renderPaginationInput(String(i + 1),
						       i,
						       this.props.paginatedArticles.page === i,
						       false))
			       }
			       {this.renderPaginationInput(">",
				       this.props.paginatedArticles.page + 1,
				       false,
				       !this.props.paginatedArticles.hasNextPage)}
		       </div>;
	}

	private renderPaginationInput(text: string, page: number, active: boolean, disabled: boolean) {
		return <a className={`item${active ? " active" : ""}${disabled ? " disabled" : ""}`}
			onClick={(e: any) => disabled ? null : this.setPageHandler(e, page)}>{text}</a>;
	}

	private renderMenu() {
        return <div className="ui">
			<div className="ui vertical text small menu">

                <div className="header item">Sort By</div>
                <a className="item">Most Relevant</a>
                <a className="item">Most Popular</a>
				<a className="active item">Newest</a>

				<div className="header item">Categories</div>

				{this.renderCategoryInput("All", undefined)}
				{newsCategories.map(category => this.renderCategoryInput(category.name, category.id))}

                <div className="header item">Other</div>
                <div className="ui small form">
                    <div className="inline field">
                        <div className="ui checkbox">
                            <input type="checkbox" className="hidden" />
                            <label>Business</label>
                        </div>
                    </div>
                </div>
               </div>
           </div>;
	}

	private renderCategoryInput(text: string, id?: number) {
		return <a className={`item ${this.props.searchConfiguration.categoryId === id ? "active" : ""}`}
			onClick={(e: any) => this.setCategoryHandler(e, id)}>
			       {text}
		       </a>;
	}

}

/*
private renderPagination() {
        let prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
        let nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

        return <p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={`/fetchdata/${prevStartDateIndex}`}>Previous</Link>
            <Link className='btn btn-default pull-right' to={`/fetchdata/${nextStartDateIndex}`}>Next</Link>
            {this.props.isLoading ? <span>Loading...</span> : []}
        </p>;
    }*/


export default connect(
(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(NewsReel) as typeof NewsReel

