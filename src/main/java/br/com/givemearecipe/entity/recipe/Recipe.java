package br.com.givemearecipe.entity.recipe;

import java.util.*;
import java.math.*;
import javax.persistence.*;
import javax.validation.constraints.*;
import br.ufes.inf.nemo.jbutler.ejb.persistence.PersistentObjectSupport;

/** TODO: generated by FrameWeb. Should be documented. */
@Entity
public class Recipe extends PersistentObjectSupport implements Comparable<Recipe> {
	/** Serialization id. */
	private static final long serialVersionUID = 1L;



	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private float calories;

	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private int ready_in_minutes;

	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private String recipe_pic_path;

	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private int servings;




		
		/** TODO: generated by FrameWeb. Should be documented. */
		@OneToMany(mappedBy="Source")
		private Set<Step> Target;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@OneToMany(mappedBy="Source")
		private Set<IngredientContainer> Target;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@ManyToMany
		private Set<Category> Target;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@OneToOne
		private Rating Target;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@OneToMany(mappedBy="Source")
		private Set<Step> Target;
		
	




	/** Getter for calories. */
	public float getCalories() {
		return calories;
	}
	
	/** Setter for calories. */
	public void setCalories(float calories) {
		this.calories = calories;
	}

	/** Getter for ready_in_minutes. */
	public int getReady_in_minutes() {
		return ready_in_minutes;
	}
	
	/** Setter for ready_in_minutes. */
	public void setReady_in_minutes(int ready_in_minutes) {
		this.ready_in_minutes = ready_in_minutes;
	}

	/** Getter for recipe_pic_path. */
	public String getRecipe_pic_path() {
		return recipe_pic_path;
	}
	
	/** Setter for recipe_pic_path. */
	public void setRecipe_pic_path(String recipe_pic_path) {
		this.recipe_pic_path = recipe_pic_path;
	}

	/** Getter for servings. */
	public int getServings() {
		return servings;
	}
	
	/** Setter for servings. */
	public void setServings(int servings) {
		this.servings = servings;
	}




		
		/** Getter for Target. */
		public Set<Step> getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Set<Step> Target) {
			this.Target = Target;
		}
		
	

		
		/** Getter for Target. */
		public Set<IngredientContainer> getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Set<IngredientContainer> Target) {
			this.Target = Target;
		}
		
	

		
		/** Getter for Target. */
		public Set<Category> getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Set<Category> Target) {
			this.Target = Target;
		}
		
	

		
		/** Getter for Target. */
		public Rating getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Rating Target) {
			this.Target = Target;
		}
		
	

		
		/** Getter for Target. */
		public Set<Step> getTarget() {
			return Target;
		}
		
		/** Setter for Target. */
		public void setTarget(Set<Step> Target) {
			this.Target = Target;
		}
		
	





	/** @see java.lang.Comparable#compareTo(java.lang.Object) */
	@Override
	public int compareTo(Recipe o) {
		// FIXME: auto-generated method stub		
		return super.compareTo(o);
	}
}